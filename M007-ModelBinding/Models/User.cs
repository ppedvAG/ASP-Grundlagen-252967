using System.ComponentModel.DataAnnotations;

namespace M007_ModelBinding.Models;

public class User
{
	[Range(3, 15, ErrorMessage = "Der Benutzername muss zw. 3 und 15 Zeichen haben!")]
	public string Username { get; set; }

	public string Password { get; set; }
}