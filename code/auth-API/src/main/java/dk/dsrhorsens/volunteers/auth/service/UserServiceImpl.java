package dk.dsrhorsens.volunteers.auth.service;

import dk.dsrhorsens.volunteers.auth.model.UserDAO;
import dk.dsrhorsens.volunteers.auth.repository.UserRepository;

import java.util.List;
import java.util.Optional;

public class UserServiceImpl implements UserService {
    UserRepository userRepository;

    public UserServiceImpl(UserRepository userRepository){
        this.userRepository = userRepository;
    }

    @Override
    public UserDAO create(UserDAO userDAO) {
        return userRepository.save(userDAO);
    }

    @Override
    public List<UserDAO> findAll() {
        return (List<UserDAO>) userRepository.findAll();
    }

    @Override
    public Iterable<UserDAO> findAllItr() {
        return userRepository.findAll();
    }

    @Override
    public Optional<UserDAO> findById(Long id) {
        return userRepository.findById(id);
    }

    @Override
    public UserDAO update(UserDAO userDAO) {
        return userRepository.save(userDAO);
    }

    @Override
    public void deleteById(Long id) {
        userRepository.deleteById(id);
    }
}
