namespace WebApi.Domain.Exceptions;

public class WebBadRequestException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);

public class WebUnauthorizedException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);

public class WebForbiddenException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);

public class WebNotFoundException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);

public class WebConflictException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);

public class WebInternalServerErrorException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);

public class WebServiceUnavailableException(string? message = null, Exception? innerException = null)
    : Exception(message, innerException);