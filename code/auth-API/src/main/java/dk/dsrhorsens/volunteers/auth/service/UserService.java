package dk.dsrhorsens.volunteers.auth.service;

import dk.dsrhorsens.volunteers.auth.model.UserDAO;
import org.springframework.stereotype.Service;

import java.util.List;
import java.util.Optional;

@Service
public interface UserService {
    UserDAO create(UserDAO userDAO);
    List<UserDAO> findAll();
    Iterable<UserDAO> findAllItr();
    Optional<UserDAO> findById(Long id);
    UserDAO update (UserDAO userDAO);
    void deleteById(Long id);
}
