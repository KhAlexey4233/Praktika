using PR2024.Controllers;
using PR2024.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PR2024
{
	/// <summary>
	/// Логика взаимодействия для Agent.xaml
	/// </summary>
	public partial class Agent : Window
	{
		private Connection _connection = new Connection();

		private ICollectionView _dataGridCollectionView;
		
		public Agent()
		{
			InitializeComponent();
			var agents = _connection.PR.Agents.ToList();
			AgentsDataGrid.ItemsSource = _dataGridCollectionView = CollectionViewSource.GetDefaultView(agents);
			_dataGridCollectionView = CollectionViewSource.GetDefaultView(agents);
		}

		AgentController agentController = new AgentController();

		int currentId = 0;
		private void AgentsDataGrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if (AgentsDataGrid.SelectedItem != null)
			{
				Agents selectedRow = (Agents)AgentsDataGrid.SelectedItem;
				FirstNameTextBox.Text = selectedRow.FirstName;
				LastNameTextBox.Text = selectedRow.LastName;
				PatronymicTextBox.Text = selectedRow.MiddleName;
				DealPercentTextBox.Text = selectedRow.DealShare.ToString();
				currentId = Convert.ToInt32(selectedRow.Id_Agent.ToString());
			}
		}
		private void Update_Click(object sender, RoutedEventArgs e)
		{
			if (currentId != 0 & AgentsDataGrid.SelectedItem != null)
			{
				Agents agent = new Agents();
				agent.FirstName = FirstNameTextBox.Text;
				agent.LastName = LastNameTextBox.Text;
				agent.MiddleName = PatronymicTextBox.Text;
				agent.DealShare = Convert.ToInt32(DealPercentTextBox.Text);
				agentController.UpdateAgent(currentId, agent);
				_connection = new Connection();
				var agentss = _connection.PR.Agents.ToList();
				AgentsDataGrid.ItemsSource = _dataGridCollectionView = CollectionViewSource.GetDefaultView(agentss); ;
				FirstNameTextBox.Text = null;
				LastNameTextBox.Text = null;
				PatronymicTextBox.Text = null;
				DealPercentTextBox.Text = null;
				currentId = 0;
			}
			else if (AgentsDataGrid.SelectedItem == null)
			{
				MessageBox.Show("Не выбран агент!");
			}
		}

		private void Delete_Click(object sender, RoutedEventArgs e)
		{
			if (currentId != 0 & AgentsDataGrid.SelectedItem != null)
			{
				agentController.DeleteAgent(currentId);
				_connection = new Connection();
				var agents = _connection.PR.Agents.ToList();
				AgentsDataGrid.ItemsSource = _dataGridCollectionView = CollectionViewSource.GetDefaultView(agents);
				FirstNameTextBox.Text = null;
				LastNameTextBox.Text = null;
				PatronymicTextBox.Text = null;
				DealPercentTextBox.Text = null;
				currentId = 0;
			}
			else if (AgentsDataGrid.SelectedItem == null)
			{
				MessageBox.Show("Не выбран агент!");
			}
		}

		private void AddWindow_Click(object sender, RoutedEventArgs e)
		{
			
			Agents agent = new Agents();
			agent.FirstName = FirstNameTextBox.Text;
			agent.LastName = LastNameTextBox.Text;
			agent.MiddleName = PatronymicTextBox.Text;
			agent.DealShare = Convert.ToInt32(DealPercentTextBox.Text);
			agentController.AddAgent(agent);
			_connection = new Connection();
			var agents = _connection.PR.Agents.ToList();
			AgentsDataGrid.ItemsSource = _dataGridCollectionView = CollectionViewSource.GetDefaultView(agents);
			FirstNameTextBox.Text = null;
			LastNameTextBox.Text = null;
			PatronymicTextBox.Text = null;
			DealPercentTextBox.Text = null;
			currentId = 0;
		}
			
		private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (_dataGridCollectionView != null)
			{
				_dataGridCollectionView.Filter = item =>
				{
					if (string.IsNullOrEmpty(SearchTextBox.Text))
					{
						return true; // Если поле поиска пустое, отображаем все строки
					}

					var agent = item as Agents; // Замените "Agent" на ваш класс данных
					return agent.FirstName.Contains(SearchTextBox.Text) ||
						   agent.LastName.Contains(SearchTextBox.Text) ||
						   agent.MiddleName.Contains(SearchTextBox.Text) ||
						   agent.DealShare.ToString().Contains(SearchTextBox.Text);
				};
			}
		}


		private int LevenshteinDistance(string s1, string s2)
		{

			return 1;
		}

		private void SupplyBtn_Click(object sender, RoutedEventArgs e)
		{
			Supply supply = new Supply();
			this.Close();
			supply.Show();
		}

		private void DealBtn_Click(object sender, RoutedEventArgs e)
		{
			Deal deal = new Deal();
			this.Close();
			deal.Show();
        }
    }
}