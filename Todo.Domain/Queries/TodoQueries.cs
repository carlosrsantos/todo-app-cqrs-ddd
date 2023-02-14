using System.Linq.Expressions;
using Todo.Domain.Entities;

namespace Todo.Domain.Queries;

public static class TodoQueries
{
  public static Expression<Func<TodoItem, bool>> GetAll(string email)
  {
    return x => x.User == email;
  }

  public static Expression<Func<TodoItem, bool>> GetAllDone(string email)
  {
    return x => x.User == email && x.Done == true;
  }
  public static Expression<Func<TodoItem, bool>> GetAllUndone(string email)
  {
    return x => x.User == email && x.Done == false;
  }

  public static Expression<Func<TodoItem, bool>> GetByPeriod(string email, DateTime date, bool done)
  {
    return x => 
      x.User == email &&
      x.Done == done &&
      x.Date.Date == date.Date;
  }
}