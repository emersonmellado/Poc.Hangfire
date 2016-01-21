using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HangFireManager
{
    internal class Class1
    {
        protected class IndicadorPeriodo
        {
            public int IdpPeriodicidade { get; set; }

            public bool IdpTipoIndicador { get; set; }

            public DateTime IdpDataLimite { get; set; }

            public int IdpPeriodo { get; set; }

            public int IdpAno { get; set; }

            public bool IdpDiasUteis { get; set; }

            public int IdpPrazo { get; set; }
        }

        protected void CalculaDataLimite(ref IndicadorPeriodo model)
        {
            //IdpPeriodicidade = 1 (12/12=1) ==> anual
            //PERIODO: 1 *  model.IdpPeriodo
            //IdpPeriodicidade = 2 (12/4=3) ==> trimestral
            //PERIODO: 3 *  model.IdpPeriodo
            //IdpPeriodicidade = 3 (12/2=6)==> semestral
            //PERIODO: 6 *  model.IdpPeriodo
            //IdpPeriodicidade = 4 (12/=)==> semestral
            //PERIODO: 1
            //IdpPeriodicidade = ELSE (12/1=12) ==> mensal
            //PERIODO: 12 *  model.IdpPeriodo
            //model.IdpAno = ANO DATA LIMITE
            if (!model.IdpTipoIndicador) // Plano
            {
                switch (model.IdpPeriodicidade)
                {
                    case 1: // Mensal
                        model.IdpDataLimite = new DateTime(model.IdpAno, 1 * model.IdpPeriodo, 1);
                        break;

                    case 2: // Trimestral
                        var trimestresTotais = 3 * model.IdpPeriodo;
                        model.IdpDataLimite = new DateTime(model.IdpAno, trimestresTotais - (trimestresTotais - 1), 1);
                        break;

                    case 3: // Semestral
                        var semestresTotais = 6 * model.IdpPeriodo;
                        model.IdpDataLimite = new DateTime(model.IdpAno, semestresTotais - (semestresTotais - 1), 1);
                        break;

                    case 4: // Anual
                        model.IdpDataLimite = new DateTime(model.IdpAno, 1, 1);
                        break;

                    default:// Mensal
                        var mesesTotais = 12 * model.IdpPeriodo;
                        model.IdpDataLimite = new DateTime(model.IdpAno, mesesTotais - (mesesTotais - 1), 1);
                        break;
                }
                if (model.IdpDiasUteis)
                {
                    model.IdpDataLimite = model.IdpDataLimite.AddDays(-1);
                    //model.IdpDataLimite = new FeriadoDaoWriter().RetornarDataSemFeriado(model.IdpDataLimite, model.IdpPrazo, false);
                }
                else
                    model.IdpDataLimite = model.IdpDataLimite.AddDays(-model.IdpPrazo);
            }
            else // Indicador
            {
                switch (model.IdpPeriodicidade)
                {
                    case 1: // Mensal
                        model.IdpDataLimite = new DateTime(model.IdpAno, 1 * model.IdpPeriodo, 1);
                        break;

                    case 2: // Trimestral
                        var trimestresTotais = 3 * model.IdpPeriodo;
                        model.IdpDataLimite = new DateTime(model.IdpAno, trimestresTotais, DateTime.DaysInMonth(model.IdpAno, trimestresTotais));
                        break;

                    case 3: // Semestral
                        var semestresTotais = 6 * model.IdpPeriodo;
                        model.IdpDataLimite = new DateTime(model.IdpAno, semestresTotais, DateTime.DaysInMonth(model.IdpAno, semestresTotais));
                        break;

                    case 4: // Anual
                        model.IdpDataLimite = new DateTime(model.IdpAno, 12, 31);
                        break;

                    default:// Mensal
                        var mesesTotais = 12 * model.IdpPeriodo;
                        model.IdpDataLimite = new DateTime(model.IdpAno, mesesTotais, DateTime.DaysInMonth(model.IdpAno, mesesTotais));
                        break;
                }
                if (model.IdpDiasUteis)
                    //model.IdpDataLimite = new FeriadoDaoWriter().RetornarDataSemFeriado(model.IdpDataLimite, model.IdpPrazo, true);
                    model.IdpDataLimite = model.IdpDataLimite.AddDays(model.IdpPrazo);
                else
                    model.IdpDataLimite = model.IdpDataLimite.AddDays(model.IdpPrazo);
            }
        }
    }
}