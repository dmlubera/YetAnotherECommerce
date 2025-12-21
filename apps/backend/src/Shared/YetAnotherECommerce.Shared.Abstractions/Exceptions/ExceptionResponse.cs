using System.Net;

namespace YetAnotherECommerce.Shared.Abstractions.Exceptions;

public record ExceptionResponse(object Response, HttpStatusCode StatusCode);