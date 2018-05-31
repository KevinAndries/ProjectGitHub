using BusinessLogicLayer;
using DataAccessLayer.Model.FlexDeskDb;
using Microsoft.AspNetCore.Http;
using MVC.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC.ViewModels
{
    public class ReservationViewModel
    {
        private IEnumerable<Floor> floors;
        private Floor floor;

        public Building Building { get; set; }
        public IEnumerable<Floor> Floors {
            get { return floors; }
            set {
                floors = value;
                foreach (var f in floors)
                {
                    f.Svg = AddQuotes(f.Svg);
                }
            }
        }
        public User User { get; set; }
        public User ActiveUser { get; set; }
        public IEnumerable<ReservationFE> Reservations { get; set; }

        public long UserId { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Start { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? End { get; set;}
        public string UserCode { get; set; }

        public long ReservationId { get; set; }
        public ReservationFE ReservationUser { get; set; }
        public Floor ReservationFloor {
            get
            {
                return floor;
            }
            set
            {
                floor = value;
                floor.Svg = AddQuotes(floor.Svg);
            }
        }
        public Dictionary Dictionary { get; set; }

        public List<int?> DefaultDesks { get; set; }

        public bool DatesOK { get; set; }
        public string Message{ get; set; }



        private string AddQuotes(string svg)
        {
            string svg2 = "";
            bool newString = false;

            if (!svg.Contains('"'))
            {
                if (svg.Contains("'"))
                {
                    svg2 = AddQuotes(svg.Replace("'", ""));
                }
                else
                {
                    foreach (char c in svg)
                    {
                        switch (c)
                        {
                            case '=':
                                svg2 = svg2 + c + '"';
                                newString = true;
                                break;
                            case ',':
                                if (newString)
                                {
                                    svg2 = svg2 + '"' + ' ';
                                    newString = false;
                                }
                                else
                                {
                                    svg2 += c;
                                }
                                break;
                            case ' ':
                            case '/':
                            case '>':
                                if (newString)
                                {
                                    svg2 += '"';
                                    newString = false;
                                }
                                svg2 += c;
                                break;
                            default:
                                svg2 += c;
                                break;
                        }

                    }

                }
            }
            else
            {
                svg2 = svg;
            }

            return svg2;
        }

        public string CheckDates(int? language)
        {
            DatesOK = true;

            if (Start == null || End == null)
            {
                Start = DateTime.Today;
                End = DateTime.Today;
            }

            if (End < Start)
            {
                DatesOK = false;
                return new Dictionary(language).Label22;
            }

            if (((DateTime)Start).AddDays(6) < End)
            {
                DatesOK = false;
                return new Dictionary(language).Label23;
            }

            if (DateTime.Today.AddMonths(1)<End)
            {
                DatesOK = false;
                return new Dictionary(language).Label24 + DateTime.Today.Date.AddMonths(1).ToShortDateString();
            }

            return "";
        }

    }
}
