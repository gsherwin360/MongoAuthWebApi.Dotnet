﻿namespace MongoAuthWebApi.Primitives;

public class Result<T>
{
    public bool IsSuccess { get; set; }

    public T Value { get; set; }

    public Error Error { get; set; }

	private Result(bool isSuccess, T value, Error error)
	{
		IsSuccess = isSuccess;
		Value = value;
		Error = error;
	}

	public static Result<T> Success(T value) => new Result<T>(true, value, default!);

	public static Result<T> Failure(Error error) => new Result<T>(false, default!, error);
}