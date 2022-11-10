package dk.dsrhorsens.volunteers;

import org.springframework.jdbc.core.JdbcTemplate;

public class DataManager {
	private static DataManager instance;

	public JdbcTemplate database;

	public DataManager(JdbcTemplate database) {
		this.database = database;
	}

	public static void initialize(JdbcTemplate database) {
		if (instance == null) {
			instance = new DataManager(database);
		}
		String currentSchema = database.query("SELECT current_schema()", rs -> {rs.next();return rs.getString(1);});
		if (!currentSchema.equals("volunteermanager")) {
			System.out.println("Current schema is " + currentSchema + ", but should be 'volunteermanager'.");
			database.execute("SET SCHEMA 'volunteermanager'");
		}
	}

	public static DataManager getInstance() {
		if (instance != null) {
			return instance;
		} else {
			throw new IllegalStateException("DataManager not initialized");
		}
	}
}
