using APICatalogo.Context;
using APICatalogo.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace APICatalogo.Services
{
    public class Ola : Iola
    {
        public string chamaOla(string nome)
        {
            return $"Olá, {nome}";
        }
    }
}
