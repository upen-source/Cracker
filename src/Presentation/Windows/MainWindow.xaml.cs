using System.Collections.Generic;
using System.Linq;
using System.Windows;
using Data;
using Entities;

namespace Presentation.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow(PeopleRepository repo)
        {
            InitializeComponent();

            IList<Person> people = repo.GetAll();
            string peopleMsg = people.Select(p => p.Id)
                .Aggregate((c, n) => $"{c}\n{n}");
            MessageBox.Show(repo.FindById("333").Id);
        }


    }
}
