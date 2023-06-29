namespace AuthAPI.Models.Dtos;
using System;

	public class ResponseDto
	{
		public ResponseDto()
		{
		}

		public Object Response { get; set; }
		public bool IsSuccess { get; set; } = true;
		public string Message { get; set; } = "";

	}

