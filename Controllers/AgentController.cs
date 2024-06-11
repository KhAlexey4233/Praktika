using PR2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PR2024.Controllers
{
	public class AgentController
	{
		Connection connection = new Connection();
		public void DeleteAgent(int idAgent)
		{
			var filmDelete = connection.PR.Agents.Where(x => (x.Id_Agent == idAgent)).First();
			var demantsDelete = connection.PR.Demands.Where(x => x.Id_Agent == idAgent);
			var supplyDelete = connection.PR.Supply.Where(x => x.Id_Agent == idAgent);
			connection.PR.Agents.Remove(filmDelete);

			foreach (var demant in demantsDelete)
			{
				connection.PR.Demands.Remove(demant);
				var dealsDelete = connection.PR.Deals.Where(x => x.Id_Demand == demant.Id_Demand);
				foreach (var deal in dealsDelete)
				{
					connection.PR.Deals.Remove(deal);
				}
			}
			foreach (var sup in supplyDelete)
			{
				connection.PR.Supply.Remove(sup);
			}
			connection.PR.SaveChanges();
		}
		public void UpdateAgent(int idAgent, Agents agents)
		{
			var UpdateAgent = connection.PR.Agents.Where(x => (x.Id_Agent == idAgent)).First();
			if (UpdateAgent != null)
			{
				if (agents.FirstName != null)
				{
					connection.PR.Agents.Where(x => (x.Id_Agent == idAgent)).First().FirstName = agents.FirstName;
					connection.PR.SaveChanges();
				}
				if (agents.LastName != null)
				{
					connection.PR.Agents.Where(x => (x.Id_Agent == idAgent)).First().LastName = agents.LastName;
					connection.PR.SaveChanges();
				}
				if (agents.MiddleName != null)
				{
					connection.PR.Agents.Where(x => (x.Id_Agent == idAgent)).First().MiddleName = agents.MiddleName;
					connection.PR.SaveChanges();
				}
				if (agents.DealShare != null)
				{
					connection.PR.Agents.Where(x => (x.Id_Agent == idAgent)).First().DealShare = agents.DealShare;
					connection.PR.SaveChanges();
				}
				connection.PR.SaveChanges();
			}
		}
		public void AddAgent(Agents agent)
		{
			if (agent.FirstName != null && agent.LastName != null && agent.MiddleName != null && agent.DealShare != null)
			{
				connection.PR.Agents.Add(agent);
				connection.PR.SaveChanges();
			}
			else
			{
				MessageBox.Show("Необходимо заполнить столбцы");
			}
		}
	}
}

