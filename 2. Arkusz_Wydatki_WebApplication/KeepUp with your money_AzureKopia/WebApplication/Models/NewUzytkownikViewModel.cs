﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication.Models
{
	public class NewUzytkownikViewModel
	{
		[Display(Name = "Imię użytkownika")]
		[StringLength(30, ErrorMessage = "Imię użytkownika jest za długie")]
		public string Imie { get; set; }


		[Display(Name = "Nazwisko użytkownika")]
		[StringLength(50, ErrorMessage = "Nazwisko użytkownika jest za długie")]
		public string Nazwisko { get; set; }


		[Display(Name = "Nick użytkownika")]
		[StringLength(30, ErrorMessage = "Nick użytkownikai jest za długie")]
		public string Nick { get; set; }


		[Display(Name = "Email użytkownika")]
		[StringLength(50, ErrorMessage = "Tytuł książki jest za długi")]
		public string EMail { get; set; }
	}
}
