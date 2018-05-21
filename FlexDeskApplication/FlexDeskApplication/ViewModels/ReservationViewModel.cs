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
        public IEnumerable<Reservation> Reservations { get; set; }

        public long UserId { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set;}
        public string LoginUser { get; set; }

        public long ReservationId { get; set; }
        public Reservation ReservationUser { get; set; }
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
  
        
        private string AddQuotes(string svg)
        {
            string svg2 = "";
            bool newString = false;

            if (!svg.Contains('"'))
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
            else
            {
                svg2 = svg;
            }

            return svg2;
        }

        
    }
}
