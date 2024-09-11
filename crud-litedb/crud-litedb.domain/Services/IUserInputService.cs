using crud_litedb.Models;

namespace crud_litedb.Services;

public interface IUserInputService
{ 
    Person? FillPerson(int id = 0);

    int? GetId();

    void FlowPause();
}