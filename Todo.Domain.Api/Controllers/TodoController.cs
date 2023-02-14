using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Todo.Domain.Commands;
using Todo.Domain.Entities;
using Todo.Domain.Handlers;
using Todo.Domain.Repositories;

namespace Todo.Domain.Api.Controllers;

[ApiController]
[Route("v1/todos")]
[Authorize]
public class TodoController : ControllerBase
{
  [Route("")]
  [HttpGet]
  public IEnumerable<TodoItem> GetAll(
    [FromServices] ITodoRepository repository
  ) => repository.GetAll(user: User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value);
  
  [Route("done")]
  [HttpGet]
  public IEnumerable<TodoItem> GetAllDone(
    [FromServices] ITodoRepository repository
  ) => repository.GetAllDone(user: User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value);

  [Route("undone")]
  [HttpGet]
  public IEnumerable<TodoItem> GetAllUndone(
    [FromServices] ITodoRepository repository
  ) => repository.GetAllUndone(user: User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value);


  [Route("done/today")]
  [HttpGet]
  public IEnumerable<TodoItem> GetDoneForToday(
    [FromServices] ITodoRepository repository
  ) => repository.GetByPeriod(user: User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value, DateTime.Now, true);

  [Route("done/tomorrow")]
  [HttpGet]
  public IEnumerable<TodoItem> GetDoneForTomorrow(
    [FromServices] ITodoRepository repository
  ) => repository.GetByPeriod(user: User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value, DateTime.Now.Date.AddDays(1), true);
  
  [Route("undone/today")]
  [HttpGet]
  public IEnumerable<TodoItem> GetUndoneForToday(
    [FromServices] ITodoRepository repository
  ) => repository.GetByPeriod(user: User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value, DateTime.Now, false);

  [Route("undone/tomorrow")]
  [HttpGet]
  public IEnumerable<TodoItem> GetUndoneForTomorrow(
    [FromServices] ITodoRepository repository
  ) => repository.GetByPeriod(user: User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value, DateTime.Now.Date.AddDays(1), false);

  [Route("")]
  [HttpPost]
  public GenericCommandResult Create(
    [FromBody] CreateTodoCommand command,
    [FromServices] TodoHandler handler
  )
  {
    command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
    return (GenericCommandResult)handler.Handle(command);
  }

  [Route("")]
  [HttpPut]
  public GenericCommandResult Update(
    [FromBody] UpdateTodoCommand command,
    [FromServices] TodoHandler handler
  )
  {
    command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
    return (GenericCommandResult)handler.Handle(command);
  }

  [Route("mark-as-done")]
  [HttpPut]
  public GenericCommandResult MaskAsDone(
    [FromBody] MarkTodoAsDoneCommand command,
    [FromServices] TodoHandler handler
  )
  {
    command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
    return (GenericCommandResult)handler.Handle(command);
  }
  
  [Route("mark-as-undone")]
  [HttpPut]
  public GenericCommandResult MaskTodoAsUndone(
    [FromBody] MarkTodoAsUndoneCommand command,
    [FromServices] TodoHandler handler
  )
  {
    command.User = User.Claims.FirstOrDefault(x => x.Type == "user_id")?.Value;
    return (GenericCommandResult)handler.Handle(command);
  }
}