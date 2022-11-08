package dk.dsrhorsens.volunteers.auth.repository;

import dk.dsrhorsens.volunteers.auth.model.UserDAO;
import org.springframework.data.jpa.repository.JpaRepository;
import org.springframework.stereotype.Repository;

@Repository
public interface UserRepository extends JpaRepository<UserDAO, Long> {
}
