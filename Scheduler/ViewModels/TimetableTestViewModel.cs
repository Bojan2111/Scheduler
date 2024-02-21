using Scheduler.Models;
using Scheduler.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduler.ViewModels
{
    public class TimetableTestViewModel : ViewModelBase
    {
        private ObservableCollection<TeamSchedule> _teamSchedules;
        public ObservableCollection<TeamSchedule> TeamSchedules
        {
            get { return _teamSchedules; }
            set
            {
                _teamSchedules = value;
                OnPropertyChanged(nameof(TeamSchedule));
            }
        }

        public TimetableTestViewModel()
        {
            TeamSchedules.Add( new TeamSchedule()
            {
                Id = 98,
                TeamName = "Test 1",
                EmployeeSchedules = new List<EmployeeSchedule>
                {
                    new EmployeeSchedule()
                    {
                        Id = 234,
                        EmployeeName = "Test Tester 1",
                        EmployeeRole = "Tester",
                        Shifts = new List<Shift>
                        {
                            new Shift
                            {
                                Id = 876,
                                Name = "T",
                                EmployeeId = 234,
                                Date = new DateTime(2024, 1, 1),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 877,
                                Name = "S",
                                EmployeeId = 234,
                                Date = new DateTime(2024, 1, 2),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 878,
                                Name = "T",
                                EmployeeId = 234,
                                Date = new DateTime(2024, 1, 14),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 879,
                                Name = "S",
                                EmployeeId = 234,
                                Date = new DateTime(2024, 1, 15),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 880,
                                Name = "T",
                                EmployeeId = 234,
                                Date = new DateTime(2024, 1, 21),
                                Month = 1
                            }
                        }
                    },
                    new EmployeeSchedule()
                    {
                        Id = 235,
                        EmployeeName = "Test Tester 2",
                        EmployeeRole = "Help",
                        Shifts = new List<Shift>
                        {
                            new Shift
                            {
                                Id = 881,
                                Name = "T",
                                EmployeeId = 235,
                                Date = new DateTime(2024, 1, 1),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 882,
                                Name = "S",
                                EmployeeId = 235,
                                Date = new DateTime(2024, 1, 2),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 883,
                                Name = "T",
                                EmployeeId = 235,
                                Date = new DateTime(2024, 1, 14),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 884,
                                Name = "S",
                                EmployeeId = 235,
                                Date = new DateTime(2024, 1, 15),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 885,
                                Name = "T",
                                EmployeeId = 235,
                                Date = new DateTime(2024, 1, 21),
                                Month = 1
                            }
                        }
                    },
                    new EmployeeSchedule()
                    {
                        Id = 236,
                        EmployeeName = "Test Tester 3",
                        EmployeeRole = " ",
                        Shifts = new List<Shift>
                        {
                            new Shift
                            {
                                Id = 886,
                                Name = "T",
                                EmployeeId = 236,
                                Date = new DateTime(2024, 1, 1),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 887,
                                Name = "S",
                                EmployeeId = 236,
                                Date = new DateTime(2024, 1, 2),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 888,
                                Name = "T",
                                EmployeeId = 236,
                                Date = new DateTime(2024, 1, 14),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 889,
                                Name = "S",
                                EmployeeId = 236,
                                Date = new DateTime(2024, 1, 15),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 890,
                                Name = "T",
                                EmployeeId = 236,
                                Date = new DateTime(2024, 1, 21),
                                Month = 1
                            }
                        }
                    }
                }
            });
            TeamSchedules.Add(new TeamSchedule()
            {
                Id = 99,
                TeamName = "Test 2",
                EmployeeSchedules = new List<EmployeeSchedule>
                {
                    new EmployeeSchedule()
                    {
                        Id = 237,
                        EmployeeName = "Test Tester 4",
                        EmployeeRole = "Tester",
                        Shifts = new List<Shift>
                        {
                            new Shift
                            {
                                Id = 891,
                                Name = "T",
                                EmployeeId = 237,
                                Date = new DateTime(2024, 1, 2),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 892,
                                Name = "S",
                                EmployeeId = 237,
                                Date = new DateTime(2024, 1, 3),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 893,
                                Name = "T",
                                EmployeeId = 237,
                                Date = new DateTime(2024, 1, 15),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 894,
                                Name = "S",
                                EmployeeId = 237,
                                Date = new DateTime(2024, 1, 16),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 895,
                                Name = "T",
                                EmployeeId = 237,
                                Date = new DateTime(2024, 1, 22),
                                Month = 1
                            }
                        }
                    },
                    new EmployeeSchedule()
                    {
                        Id = 238,
                        EmployeeName = "Test Tester 5",
                        EmployeeRole = "Help",
                        Shifts = new List<Shift>
                        {
                            new Shift
                            {
                                Id = 896,
                                Name = "T",
                                EmployeeId = 238,
                                Date = new DateTime(2024, 1, 2),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 897,
                                Name = "S",
                                EmployeeId = 238,
                                Date = new DateTime(2024, 1, 3),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 898,
                                Name = "T",
                                EmployeeId = 238,
                                Date = new DateTime(2024, 1, 15),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 899,
                                Name = "S",
                                EmployeeId = 238,
                                Date = new DateTime(2024, 1, 16),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 900,
                                Name = "T",
                                EmployeeId = 238,
                                Date = new DateTime(2024, 1, 22),
                                Month = 1
                            }
                        }
                    },
                    new EmployeeSchedule()
                    {
                        Id = 239,
                        EmployeeName = "Test Tester 6",
                        EmployeeRole = " ",
                        Shifts = new List<Shift>
                        {
                            new Shift
                            {
                                Id = 901,
                                Name = "T",
                                EmployeeId = 239,
                                Date = new DateTime(2024, 1, 2),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 902,
                                Name = "S",
                                EmployeeId = 239,
                                Date = new DateTime(2024, 1, 3),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 903,
                                Name = "T",
                                EmployeeId = 239,
                                Date = new DateTime(2024, 1, 15),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 904,
                                Name = "S",
                                EmployeeId = 239,
                                Date = new DateTime(2024, 1, 16),
                                Month = 1
                            },
                            new Shift
                            {
                                Id = 905,
                                Name = "T",
                                EmployeeId = 239,
                                Date = new DateTime(2024, 1, 22),
                                Month = 1
                            }
                        }
                    }
                }
            });
        }
    }
}
