using PR2024.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PR2024.Controllers
{
	public class SupplyController
	{
		Connection connection = new Connection();
		public void DeleteSupply(int idSupply)
		{
			var sypplyDelete = connection.PR.Supply.Where(x => (x.Id_Supply == idSupply)).First();
			connection.PR.Supply.Remove(sypplyDelete);
			connection.PR.SaveChanges();
		}
		public void AddSupply(Model.Supply supply)
		{
			if (supply.Price != 0 && supply.Id_Agent != 0 && supply.Id_Client != 0 && supply.Id_RealEstate != 0)
			{
				connection.PR.Supply.Add(supply);
				connection.PR.SaveChanges();
			}
			else
			{
				MessageBox.Show("Пустые столбцы");
			}
		}
		public void UpdateSupply(int idSupply, Model.Supply supply)
		{
			var UpdateSupply = connection.PR.Supply.Where(x => (x.Id_Supply == idSupply)).First();
			if (UpdateSupply != null)
			{
				if (supply.Price != 0)
				{
					connection.PR.Supply.Where(x => (x.Id_Supply == idSupply)).First().Price = supply.Price;
					connection.PR.SaveChanges();
				}
				if (supply.Id_Agent != 0)
				{
					connection.PR.Supply.Where(x => (x.Id_Supply == idSupply)).First().Id_Agent = supply.Id_Agent;
					connection.PR.SaveChanges();
				}
				if (supply.Id_Client != 0)
				{
					connection.PR.Supply.Where(x => (x.Id_Supply == idSupply)).First().Id_Client = supply.Id_Client;
					connection.PR.SaveChanges();
				}
				if (supply.Id_RealEstate != 0)
				{
					connection.PR.Supply.Where(x => (x.Id_Supply == idSupply)).First().Id_RealEstate = supply.Id_RealEstate;
					connection.PR.SaveChanges();
				}
				connection.PR.SaveChanges();
			}
		}
	}
}
