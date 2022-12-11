package dk.dsrhorsens.volunteers.proto.event;

import static io.grpc.MethodDescriptor.generateFullMethodName;
import static io.grpc.stub.ClientCalls.asyncBidiStreamingCall;
import static io.grpc.stub.ClientCalls.asyncClientStreamingCall;
import static io.grpc.stub.ClientCalls.asyncServerStreamingCall;
import static io.grpc.stub.ClientCalls.asyncUnaryCall;
import static io.grpc.stub.ClientCalls.blockingServerStreamingCall;
import static io.grpc.stub.ClientCalls.blockingUnaryCall;
import static io.grpc.stub.ClientCalls.futureUnaryCall;
import static io.grpc.stub.ServerCalls.asyncBidiStreamingCall;
import static io.grpc.stub.ServerCalls.asyncClientStreamingCall;
import static io.grpc.stub.ServerCalls.asyncServerStreamingCall;
import static io.grpc.stub.ServerCalls.asyncUnaryCall;
import static io.grpc.stub.ServerCalls.asyncUnimplementedStreamingCall;
import static io.grpc.stub.ServerCalls.asyncUnimplementedUnaryCall;

/**
 */
@javax.annotation.Generated(
    value = "by gRPC proto compiler (version 1.24.0)",
    comments = "Source: event.proto")
public final class EventServiceGrpc {

  private EventServiceGrpc() {}

  public static final String SERVICE_NAME = "dsr.event.EventService";

  // Static method descriptors that strictly reflect the proto.
  private static volatile io.grpc.MethodDescriptor<EventInfo,
      EventId> getCreateEventMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "createEvent",
      requestType = EventInfo.class,
      responseType = EventId.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<EventInfo,
      EventId> getCreateEventMethod() {
    io.grpc.MethodDescriptor<EventInfo, EventId> getCreateEventMethod;
    if ((getCreateEventMethod = EventServiceGrpc.getCreateEventMethod) == null) {
      synchronized (EventServiceGrpc.class) {
        if ((getCreateEventMethod = EventServiceGrpc.getCreateEventMethod) == null) {
          EventServiceGrpc.getCreateEventMethod = getCreateEventMethod =
              io.grpc.MethodDescriptor.<EventInfo, EventId>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "createEvent"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  EventInfo.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  EventId.getDefaultInstance()))
              .setSchemaDescriptor(new EventServiceMethodDescriptorSupplier("createEvent"))
              .build();
        }
      }
    }
    return getCreateEventMethod;
  }

  private static volatile io.grpc.MethodDescriptor<EventId,
      IndexedEvent> getRetrieveEventMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "retrieveEvent",
      requestType = EventId.class,
      responseType = IndexedEvent.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<EventId,
      IndexedEvent> getRetrieveEventMethod() {
    io.grpc.MethodDescriptor<EventId, IndexedEvent> getRetrieveEventMethod;
    if ((getRetrieveEventMethod = EventServiceGrpc.getRetrieveEventMethod) == null) {
      synchronized (EventServiceGrpc.class) {
        if ((getRetrieveEventMethod = EventServiceGrpc.getRetrieveEventMethod) == null) {
          EventServiceGrpc.getRetrieveEventMethod = getRetrieveEventMethod =
              io.grpc.MethodDescriptor.<EventId, IndexedEvent>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "retrieveEvent"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  EventId.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  IndexedEvent.getDefaultInstance()))
              .setSchemaDescriptor(new EventServiceMethodDescriptorSupplier("retrieveEvent"))
              .build();
        }
      }
    }
    return getRetrieveEventMethod;
  }

  private static volatile io.grpc.MethodDescriptor<IndexedEvent,
      com.google.protobuf.Empty> getUpdateEventMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "updateEvent",
      requestType = IndexedEvent.class,
      responseType = com.google.protobuf.Empty.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<IndexedEvent,
      com.google.protobuf.Empty> getUpdateEventMethod() {
    io.grpc.MethodDescriptor<IndexedEvent, com.google.protobuf.Empty> getUpdateEventMethod;
    if ((getUpdateEventMethod = EventServiceGrpc.getUpdateEventMethod) == null) {
      synchronized (EventServiceGrpc.class) {
        if ((getUpdateEventMethod = EventServiceGrpc.getUpdateEventMethod) == null) {
          EventServiceGrpc.getUpdateEventMethod = getUpdateEventMethod =
              io.grpc.MethodDescriptor.<IndexedEvent, com.google.protobuf.Empty>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "updateEvent"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  IndexedEvent.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
              .setSchemaDescriptor(new EventServiceMethodDescriptorSupplier("updateEvent"))
              .build();
        }
      }
    }
    return getUpdateEventMethod;
  }

  private static volatile io.grpc.MethodDescriptor<EventId,
      com.google.protobuf.Empty> getDeleteEventMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "deleteEvent",
      requestType = EventId.class,
      responseType = com.google.protobuf.Empty.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<EventId,
      com.google.protobuf.Empty> getDeleteEventMethod() {
    io.grpc.MethodDescriptor<EventId, com.google.protobuf.Empty> getDeleteEventMethod;
    if ((getDeleteEventMethod = EventServiceGrpc.getDeleteEventMethod) == null) {
      synchronized (EventServiceGrpc.class) {
        if ((getDeleteEventMethod = EventServiceGrpc.getDeleteEventMethod) == null) {
          EventServiceGrpc.getDeleteEventMethod = getDeleteEventMethod =
              io.grpc.MethodDescriptor.<EventId, com.google.protobuf.Empty>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "deleteEvent"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  EventId.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
              .setSchemaDescriptor(new EventServiceMethodDescriptorSupplier("deleteEvent"))
              .build();
        }
      }
    }
    return getDeleteEventMethod;
  }

  private static volatile io.grpc.MethodDescriptor<com.google.protobuf.Empty,
      EventList> getListEventsMethod;

  @io.grpc.stub.annotations.RpcMethod(
      fullMethodName = SERVICE_NAME + '/' + "listEvents",
      requestType = com.google.protobuf.Empty.class,
      responseType = EventList.class,
      methodType = io.grpc.MethodDescriptor.MethodType.UNARY)
  public static io.grpc.MethodDescriptor<com.google.protobuf.Empty,
      EventList> getListEventsMethod() {
    io.grpc.MethodDescriptor<com.google.protobuf.Empty, EventList> getListEventsMethod;
    if ((getListEventsMethod = EventServiceGrpc.getListEventsMethod) == null) {
      synchronized (EventServiceGrpc.class) {
        if ((getListEventsMethod = EventServiceGrpc.getListEventsMethod) == null) {
          EventServiceGrpc.getListEventsMethod = getListEventsMethod =
              io.grpc.MethodDescriptor.<com.google.protobuf.Empty, EventList>newBuilder()
              .setType(io.grpc.MethodDescriptor.MethodType.UNARY)
              .setFullMethodName(generateFullMethodName(SERVICE_NAME, "listEvents"))
              .setSampledToLocalTracing(true)
              .setRequestMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  com.google.protobuf.Empty.getDefaultInstance()))
              .setResponseMarshaller(io.grpc.protobuf.ProtoUtils.marshaller(
                  EventList.getDefaultInstance()))
              .setSchemaDescriptor(new EventServiceMethodDescriptorSupplier("listEvents"))
              .build();
        }
      }
    }
    return getListEventsMethod;
  }

  /**
   * Creates a new async stub that supports all call types for the service
   */
  public static EventServiceStub newStub(io.grpc.Channel channel) {
    return new EventServiceStub(channel);
  }

  /**
   * Creates a new blocking-style stub that supports unary and streaming output calls on the service
   */
  public static EventServiceBlockingStub newBlockingStub(
      io.grpc.Channel channel) {
    return new EventServiceBlockingStub(channel);
  }

  /**
   * Creates a new ListenableFuture-style stub that supports unary calls on the service
   */
  public static EventServiceFutureStub newFutureStub(
      io.grpc.Channel channel) {
    return new EventServiceFutureStub(channel);
  }

  /**
   */
  public static abstract class EventServiceImplBase implements io.grpc.BindableService {

    /**
     */
    public void createEvent(EventInfo request,
                            io.grpc.stub.StreamObserver<EventId> responseObserver) {
      asyncUnimplementedUnaryCall(getCreateEventMethod(), responseObserver);
    }

    /**
     */
    public void retrieveEvent(EventId request,
                              io.grpc.stub.StreamObserver<IndexedEvent> responseObserver) {
      asyncUnimplementedUnaryCall(getRetrieveEventMethod(), responseObserver);
    }

    /**
     */
    public void updateEvent(IndexedEvent request,
                            io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnimplementedUnaryCall(getUpdateEventMethod(), responseObserver);
    }

    /**
     */
    public void deleteEvent(EventId request,
                            io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnimplementedUnaryCall(getDeleteEventMethod(), responseObserver);
    }

    /**
     */
    public void listEvents(com.google.protobuf.Empty request,
        io.grpc.stub.StreamObserver<EventList> responseObserver) {
      asyncUnimplementedUnaryCall(getListEventsMethod(), responseObserver);
    }

    @Override public final io.grpc.ServerServiceDefinition bindService() {
      return io.grpc.ServerServiceDefinition.builder(getServiceDescriptor())
          .addMethod(
            getCreateEventMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                EventInfo,
                EventId>(
                  this, METHODID_CREATE_EVENT)))
          .addMethod(
            getRetrieveEventMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                EventId,
                IndexedEvent>(
                  this, METHODID_RETRIEVE_EVENT)))
          .addMethod(
            getUpdateEventMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                IndexedEvent,
                com.google.protobuf.Empty>(
                  this, METHODID_UPDATE_EVENT)))
          .addMethod(
            getDeleteEventMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                EventId,
                com.google.protobuf.Empty>(
                  this, METHODID_DELETE_EVENT)))
          .addMethod(
            getListEventsMethod(),
            asyncUnaryCall(
              new MethodHandlers<
                com.google.protobuf.Empty,
                EventList>(
                  this, METHODID_LIST_EVENTS)))
          .build();
    }
  }

  /**
   */
  public static final class EventServiceStub extends io.grpc.stub.AbstractStub<EventServiceStub> {
    private EventServiceStub(io.grpc.Channel channel) {
      super(channel);
    }

    private EventServiceStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @Override
    protected EventServiceStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new EventServiceStub(channel, callOptions);
    }

    /**
     */
    public void createEvent(EventInfo request,
                            io.grpc.stub.StreamObserver<EventId> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getCreateEventMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void retrieveEvent(EventId request,
                              io.grpc.stub.StreamObserver<IndexedEvent> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getRetrieveEventMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void updateEvent(IndexedEvent request,
                            io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getUpdateEventMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void deleteEvent(EventId request,
                            io.grpc.stub.StreamObserver<com.google.protobuf.Empty> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getDeleteEventMethod(), getCallOptions()), request, responseObserver);
    }

    /**
     */
    public void listEvents(com.google.protobuf.Empty request,
        io.grpc.stub.StreamObserver<EventList> responseObserver) {
      asyncUnaryCall(
          getChannel().newCall(getListEventsMethod(), getCallOptions()), request, responseObserver);
    }
  }

  /**
   */
  public static final class EventServiceBlockingStub extends io.grpc.stub.AbstractStub<EventServiceBlockingStub> {
    private EventServiceBlockingStub(io.grpc.Channel channel) {
      super(channel);
    }

    private EventServiceBlockingStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @Override
    protected EventServiceBlockingStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new EventServiceBlockingStub(channel, callOptions);
    }

    /**
     */
    public EventId createEvent(EventInfo request) {
      return blockingUnaryCall(
          getChannel(), getCreateEventMethod(), getCallOptions(), request);
    }

    /**
     */
    public IndexedEvent retrieveEvent(EventId request) {
      return blockingUnaryCall(
          getChannel(), getRetrieveEventMethod(), getCallOptions(), request);
    }

    /**
     */
    public com.google.protobuf.Empty updateEvent(IndexedEvent request) {
      return blockingUnaryCall(
          getChannel(), getUpdateEventMethod(), getCallOptions(), request);
    }

    /**
     */
    public com.google.protobuf.Empty deleteEvent(EventId request) {
      return blockingUnaryCall(
          getChannel(), getDeleteEventMethod(), getCallOptions(), request);
    }

    /**
     */
    public EventList listEvents(com.google.protobuf.Empty request) {
      return blockingUnaryCall(
          getChannel(), getListEventsMethod(), getCallOptions(), request);
    }
  }

  /**
   */
  public static final class EventServiceFutureStub extends io.grpc.stub.AbstractStub<EventServiceFutureStub> {
    private EventServiceFutureStub(io.grpc.Channel channel) {
      super(channel);
    }

    private EventServiceFutureStub(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      super(channel, callOptions);
    }

    @Override
    protected EventServiceFutureStub build(io.grpc.Channel channel,
        io.grpc.CallOptions callOptions) {
      return new EventServiceFutureStub(channel, callOptions);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<EventId> createEvent(
        EventInfo request) {
      return futureUnaryCall(
          getChannel().newCall(getCreateEventMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<IndexedEvent> retrieveEvent(
        EventId request) {
      return futureUnaryCall(
          getChannel().newCall(getRetrieveEventMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.google.protobuf.Empty> updateEvent(
        IndexedEvent request) {
      return futureUnaryCall(
          getChannel().newCall(getUpdateEventMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<com.google.protobuf.Empty> deleteEvent(
        EventId request) {
      return futureUnaryCall(
          getChannel().newCall(getDeleteEventMethod(), getCallOptions()), request);
    }

    /**
     */
    public com.google.common.util.concurrent.ListenableFuture<EventList> listEvents(
        com.google.protobuf.Empty request) {
      return futureUnaryCall(
          getChannel().newCall(getListEventsMethod(), getCallOptions()), request);
    }
  }

  private static final int METHODID_CREATE_EVENT = 0;
  private static final int METHODID_RETRIEVE_EVENT = 1;
  private static final int METHODID_UPDATE_EVENT = 2;
  private static final int METHODID_DELETE_EVENT = 3;
  private static final int METHODID_LIST_EVENTS = 4;

  private static final class MethodHandlers<Req, Resp> implements
      io.grpc.stub.ServerCalls.UnaryMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ServerStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.ClientStreamingMethod<Req, Resp>,
      io.grpc.stub.ServerCalls.BidiStreamingMethod<Req, Resp> {
    private final EventServiceImplBase serviceImpl;
    private final int methodId;

    MethodHandlers(EventServiceImplBase serviceImpl, int methodId) {
      this.serviceImpl = serviceImpl;
      this.methodId = methodId;
    }

    @Override
    @SuppressWarnings("unchecked")
    public void invoke(Req request, io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        case METHODID_CREATE_EVENT:
          serviceImpl.createEvent((EventInfo) request,
              (io.grpc.stub.StreamObserver<EventId>) responseObserver);
          break;
        case METHODID_RETRIEVE_EVENT:
          serviceImpl.retrieveEvent((EventId) request,
              (io.grpc.stub.StreamObserver<IndexedEvent>) responseObserver);
          break;
        case METHODID_UPDATE_EVENT:
          serviceImpl.updateEvent((IndexedEvent) request,
              (io.grpc.stub.StreamObserver<com.google.protobuf.Empty>) responseObserver);
          break;
        case METHODID_DELETE_EVENT:
          serviceImpl.deleteEvent((EventId) request,
              (io.grpc.stub.StreamObserver<com.google.protobuf.Empty>) responseObserver);
          break;
        case METHODID_LIST_EVENTS:
          serviceImpl.listEvents((com.google.protobuf.Empty) request,
              (io.grpc.stub.StreamObserver<EventList>) responseObserver);
          break;
        default:
          throw new AssertionError();
      }
    }

    @Override
    @SuppressWarnings("unchecked")
    public io.grpc.stub.StreamObserver<Req> invoke(
        io.grpc.stub.StreamObserver<Resp> responseObserver) {
      switch (methodId) {
        default:
          throw new AssertionError();
      }
    }
  }

  private static abstract class EventServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoFileDescriptorSupplier, io.grpc.protobuf.ProtoServiceDescriptorSupplier {
    EventServiceBaseDescriptorSupplier() {}

    @Override
    public com.google.protobuf.Descriptors.FileDescriptor getFileDescriptor() {
      return Event.getDescriptor();
    }

    @Override
    public com.google.protobuf.Descriptors.ServiceDescriptor getServiceDescriptor() {
      return getFileDescriptor().findServiceByName("EventService");
    }
  }

  private static final class EventServiceFileDescriptorSupplier
      extends EventServiceBaseDescriptorSupplier {
    EventServiceFileDescriptorSupplier() {}
  }

  private static final class EventServiceMethodDescriptorSupplier
      extends EventServiceBaseDescriptorSupplier
      implements io.grpc.protobuf.ProtoMethodDescriptorSupplier {
    private final String methodName;

    EventServiceMethodDescriptorSupplier(String methodName) {
      this.methodName = methodName;
    }

    @Override
    public com.google.protobuf.Descriptors.MethodDescriptor getMethodDescriptor() {
      return getServiceDescriptor().findMethodByName(methodName);
    }
  }

  private static volatile io.grpc.ServiceDescriptor serviceDescriptor;

  public static io.grpc.ServiceDescriptor getServiceDescriptor() {
    io.grpc.ServiceDescriptor result = serviceDescriptor;
    if (result == null) {
      synchronized (EventServiceGrpc.class) {
        result = serviceDescriptor;
        if (result == null) {
          serviceDescriptor = result = io.grpc.ServiceDescriptor.newBuilder(SERVICE_NAME)
              .setSchemaDescriptor(new EventServiceFileDescriptorSupplier())
              .addMethod(getCreateEventMethod())
              .addMethod(getRetrieveEventMethod())
              .addMethod(getUpdateEventMethod())
              .addMethod(getDeleteEventMethod())
              .addMethod(getListEventsMethod())
              .build();
        }
      }
    }
    return result;
  }
}
