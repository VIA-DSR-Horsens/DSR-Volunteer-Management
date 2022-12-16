package dk.dsrhorsens.volunteers.service.event;

import dk.dsrhorsens.volunteers.DsrVolunteerApplication;
import dk.dsrhorsens.volunteers.proto.event.EventInfo;
import dk.dsrhorsens.volunteers.proto.event.EventInfoOrBuilder;
import dk.dsrhorsens.volunteers.proto.event.EventServiceGrpc;
import dk.dsrhorsens.volunteers.proto.event.NewEvent;
import dk.dsrhorsens.volunteers.service.dto.Event;
import dk.dsrhorsens.volunteers.service.dto.Manager;
import dk.dsrhorsens.volunteers.service.user.ManagerHttp;
import io.grpc.stub.StreamObserver;
import org.lognet.springboot.grpc.GRpcService;

import java.util.ArrayList;
import java.util.List;

@GRpcService
public class EventImpl extends EventServiceGrpc.EventServiceImplBase {
    @Override
    public void createEvent(NewEvent request, StreamObserver<EventInfo> responseObserver) {
        System.out.println("Received a create event request! "+request.toString());
        var uuid = DsrVolunteerApplication.authenticationClient.verifyCookie(request.getAuthenticationCookie());

        if (uuid == null) {
            responseObserver.onError(new Throwable("401 Invalid cookie!"));
        }

        Manager manager;
        try {
            manager = ManagerHttp.getManagerByVolunteer(uuid);
        } catch (Exception e) {
            var errorCode = Integer.parseInt(e.getMessage().substring(0,3));
            // server error
            if (errorCode >= 500) {
                responseObserver.onError(new Throwable(e.getMessage()));
                return;
            }
            // not a manager
            responseObserver.onError(new Throwable("403 Volunteer is not a manager or administrator!"));
            return;
        }

        // volunteer is a manager
        var reqMsg = new Event();
        reqMsg.setEventName(request.getNewEvent().getEventName());
        reqMsg.setDate(request.getNewEvent().getEventDate());
        var managers = new ArrayList<String>();
        for (long mId: request.getNewEvent().getManagerIdList()) {
            managers.add(mId+"");
        }
        reqMsg.setManagers(managers);

        try {
            var created = EventHttp.createNewEvent(reqMsg);
            var respMsg = EventInfo.newBuilder()
                    .setEventName(created.getEventName())
                    .setEventId(Long.parseLong(created.getEventId()))
                    .setEventDate(created.getDate());
            for (String mId: created.getManagers()) {
                respMsg.addManagerId(Long.parseLong(mId));
            }
            responseObserver.onNext(respMsg.build());
        } catch (Exception e) {
            var errorCode = Integer.parseInt(e.getMessage().substring(0,3));
            // server error
            if (errorCode >= 500) {
                responseObserver.onError(new Throwable(e.getMessage()));
                return;
            }
            // user error
            responseObserver.onError(new Throwable(e.getMessage()));
            return;
        }
    }
}
