using System;

namespace Blockbuster.Domain.Models
{
    public class Either<TError, TSuccess>
    {
        public bool IsSuccess { get; private set; }
        public bool IsFailure => !IsSuccess;
        public int StatusCode { get; private set; }
        public TSuccess Value { get; private set; }
        public TError Error { get; private set; }

        public Either()
        {
        }

        protected Either(bool isSuccess, TSuccess success, TError error, int statusCode)
        {
            IsSuccess = isSuccess;
            Value = success;
            Error = error;
            StatusCode = statusCode;
        }

        protected Either(TError error, int statusCode)
        {
            IsSuccess = false;
            Error = error;
            StatusCode = statusCode;
        }

        protected Either(TSuccess success, int statusCode)
        {
            IsSuccess = true;
            Value = success;
            StatusCode = statusCode;
        }


        public Either<TError, TSuccess> Unauthorized(TError error)
        {
            return new Either<TError, TSuccess>(error, 401);
        }

        public Either<TError, TSuccess> Forbidden(TError error)
        {
            return new Either<TError, TSuccess>(error, 403);
        }

        public Either<TError, TSuccess> BadRequest(TError error)
        {
            return new Either<TError, TSuccess>(error, 400);
        }

        public Either<TError, TSuccess> NotFound(TError error)
        {
            return new Either<TError, TSuccess>(error, 404);
        }

        public Either<TError, TSuccess> Conflict(TError error)
        {
            return new Either<TError, TSuccess>(error, 409);
        }

        public Either<TError, TSuccess> Ok(TSuccess success)
        {
            return new Either<TError, TSuccess>(success, 200);
        }

        public Either<TError, TSuccess> Created(TSuccess success)
        {
            return new Either<TError, TSuccess>(success, 200);
        }

        public Either<TError, TSuccess> CustomError(TError error, int statusCode)
        {
            return new Either<TError, TSuccess>(error, statusCode);
        }

        public Either<TError, TSuccess> CustomSuccess(TSuccess success, int statusCode)
        {
            return new Either<TError, TSuccess>(success, statusCode);
        }
    }
}