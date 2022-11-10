package dk.dsrhorsens.volunteers.data;

public class EventDAO {
	private int id;
	private String name;
	private String starttime;
	private String endtime;

	public EventDAO(int id, String name, String starttime, String endtime) {
		this.id = id;
		this.name = name;
		this.starttime = starttime;
		this.endtime = endtime;
	}

	public int getId() {
		return id;
	}

	public String getName() {
		return name;
	}

	public String getStarttime() {
		return starttime;
	}

	public String getEndtime() {
		return endtime;
	}
}
