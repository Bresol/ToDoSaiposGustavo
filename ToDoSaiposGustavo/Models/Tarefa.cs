using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ToDoSaiposGustavo.Models
{
    [Table("Tarefa")]
    public class Tarefa
    {
        [Display(Name = "ID")]
        [Column("ID")]
        public int ID { get; set; }
        [Display(Name = "Descrição")]
        [Column("Descricao")]
        public string Descricao { get; set; }
        [Display(Name = "Nome Responsável")]
        [Column("NomeResponsavel")]
        public string NomeResponsavel { get; set; }
        
        [Display(Name = "E-mail Responsável")]
        [Column("EmailResponsavel")]
        public string EmailResponsavel { get; set; }

        [Display(Name = "Status")]
        [Column("Status")]
        //0 Pendente
        //1 Concluida
        public int Status { get; set; }

        [Display(Name = "QtdPendente")]
        [Column("QtdPendente")]
        public int QtdPendente { get; set; }
    }
}
