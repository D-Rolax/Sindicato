using Sindicato.common.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sindicato.prism.Helpers
{
    public class CombosHelper
    {
        public static List<Comment> GetComments()
        {
            return new List<Comment>
            {
                        new Comment { Id = 1, Name = "Muy buen servicio" },
                        new Comment { Id = 2, Name = "Conductor muy amable" },
                        new Comment { Id = 2, Name = "Auto limpio" },
                        new Comment { Id = 2, Name = "Auto sucio o en mal estado" },
                        new Comment { Id = 2, Name = "Mal conductor" },
                        new Comment { Id = 2, Name = "Cobro mayor a lo esperado" }
            };
        }
    }
}
