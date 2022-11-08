package dk.dsrhorsens.volunteers.auth.repository;

import dk.dsrhorsens.volunteers.auth.model.UserDAO0;
import org.springframework.stereotype.Repository;

import java.util.*;

@Repository
public class UserRepository0 {
    private static final Map<Long, UserDAO0> USER_DAO_0_MAP = new HashMap<>();
    static {
        initDataSource();
    }
   private static void initDataSource(){
        UserDAO0 u1 = new UserDAO0(1L,"username","******");
        UserDAO0 u2 = new UserDAO0(2L, "username2","*****");

        USER_DAO_0_MAP.put(u1.getId(),u1);
        USER_DAO_0_MAP.put(u2.getId(),u2);
   }

   public UserDAO0 save(UserDAO0 userDAO0){
        USER_DAO_0_MAP.put(userDAO0.getId(),userDAO0);
        return userDAO0;
   }
    public UserDAO0 findById(Long id){
        return USER_DAO_0_MAP.get(id);
    }
    public UserDAO0 update(UserDAO0 userDAO0){
        USER_DAO_0_MAP.put(userDAO0.getId(),userDAO0);
        return userDAO0;
    }
    public void deleteById(Long id){
        USER_DAO_0_MAP.remove(id);
    }
    public List<UserDAO0> findAll(){
        Collection<UserDAO0> d = USER_DAO_0_MAP.values();
        List<UserDAO0> userList = new ArrayList<>();
        userList.addAll(d);
        return userList;
    }

}
