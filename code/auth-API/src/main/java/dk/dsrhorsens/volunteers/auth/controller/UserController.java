package dk.dsrhorsens.volunteers.auth.controller;

import dk.dsrhorsens.volunteers.auth.service.UserServiceImpl;
import org.slf4j.Logger;
import org.slf4j.LoggerFactory;
import org.springframework.http.ResponseEntity;
import org.springframework.web.bind.annotation.PostMapping;
import org.springframework.web.bind.annotation.RequestMapping;
import org.springframework.web.bind.annotation.RestController;

@RestController
@RequestMapping("/api/")
public class UserController {
    private Logger logger = LoggerFactory.getLogger(UserController.class);
    private UserServiceImpl userService;
    public UserController(UserServiceImpl userService){
        this.userService = userService;
    }
    @PostMapping("/user")
    public ResponseEntity<Object> c
}

