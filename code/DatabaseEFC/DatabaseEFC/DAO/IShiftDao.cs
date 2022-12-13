using DatabaseEFC.Utils;

namespace DatabaseEFC.DAO;

/// <summary>
/// Used to execute queries related to shifts
/// </summary>
public interface IShiftDao
{
    /// <summary>
    /// Creates a new shift in the database
    /// </summary>
    /// <param name="shift">The shift to create</param>
    /// <returns>The created shift</returns>
    Task<Shift> CreateAsync(DTO.Shift shift);
    /// <summary>
    /// Updates the shift in database with new values
    /// </summary>
    /// <param name="shift">The shift to update</param>
    /// <returns>The updated shift</returns>
    Task<Shift> UpdateAsync(DTO.Shift shift);
    /// <summary>
    /// Gets a shift by it's id
    /// </summary>
    /// <param name="shiftId">The id of the shift to get</param>
    /// <returns>The shift, if found</returns>
    Task<Shift> GetAsync(long shiftId);
}