using EvaluacionAcademia.NET.DataAccess.Repositories.Interfaces;
using EvaluacionAcademia.NET.Entities;
using Microsoft.EntityFrameworkCore;

namespace EvaluacionAcademia.NET.DataAccess.Repositories
{
	public class AccountCriptoRepository : Repository<AccountCripto>, IAccountCriptoRepository
	{

		public AccountCriptoRepository(ApplicationDbContext context) : base(context)
		{

		}

		/// <summary>
		/// Devuelve todas las cuentas de tipo cripto
		/// </summary>
		/// <returns>Listado de cuentas cripto</returns>
		public override async Task<List<AccountCripto>> GetAllActive()
		{
			//return await _context.CriptoAccounts.Where(s => s.IsActive == true).ToListAsync();
			return await _context.CriptoAccounts.Include(account => account.User).Where(account => account.IsActive == true).ToListAsync();
		}

        /// <summary>
        /// Devuelve saldo de Btc de cuenta cripto
        /// </summary>
        /// <param name="accountId"></param>
        /// <returns>saldo de Btc de cuenta cripto</returns>
        public async Task<decimal> GetBalance(int accountId)
		{
			var account = await _context.Accounts.OfType<AccountCripto>().FirstOrDefaultAsync(a => a.CodAccount == accountId);

			if (account != null)
			{
				return account.BalanceBtc;
			}

			return 0;
		}

        /// <summary>
        /// booleando si existe cuenta cripto segun UUID
        /// </summary>
        /// <param name="UUID"></param>
        /// <returns>verdadero si existe cuenta cripto segun UUID</returns>
        public async Task<bool> AccountExByUUID(string UUID)
		{
			return await _context.CriptoAccounts.AnyAsync(x => x.DirectionUUID == UUID);
		}

        /// <summary>
        /// booleando si existe cuenta cripto segun UserId
        /// </summary>
        /// <param name="UUID"></param>
        /// <returns>verdadero si existe cuenta cripto segun UserId</returns>
        public async Task<bool> AccountExByUserId(int userId)
		{
			return await _context.CriptoAccounts.AnyAsync(x => x.CodUser == userId && x.IsActive == true);
		}

        /// <summary>
        /// booleando si existe cuenta cripto segun codigo de cuenta
        /// </summary>
        /// <param name="id"></param>
        /// <returns>verdadero si existe cuenta cripto segun codigo de cuenta</returns>
        public async Task<bool> AccountExById(int id)
		{
			return await _context.CriptoAccounts.AnyAsync(x => (x.CodAccount == id && x.Type== "Cripto"));
		}

        /// <summary>
        /// Devuelve cuenta cripto segun Id
        /// </summary>
        /// <param name="accountToGet"></param>
        /// <returns>cuenta cripto segun Id</returns>
        public override async Task<AccountCripto> GetById(AccountCripto accountToGet)
		{
			var account = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.CodAccount == accountToGet.CodAccount);

			return account;
		}

		/// <summary>
		/// Actualiza la cuenta cripto
		/// </summary>
		/// <param name="updateAccount"></param>
		/// <returns>booleano de si se pudo o no actualizar</returns>
		public override async Task<bool> Update(AccountCripto updateAccount)
		{
			var account = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.CodAccount == updateAccount.CodAccount);
			if (account == null) { return false; }
			account.Type = updateAccount.Type;
			account.CodUser = updateAccount.CodUser;
			account.IsActive = true;
			account.DirectionUUID = updateAccount.DirectionUUID;
			account.BalanceBtc = updateAccount.BalanceBtc;

			_context.CriptoAccounts.Update(account);
			return true;
		}

        /// <summary>
        /// Devuelve cuenta cripto segun UUID
        /// </summary>
        /// <param name="UUID"></param>
        /// <returns>cuenta cripto segun UUID</returns>
        public async Task<AccountCripto> GetByUUID(string UUID)
		{
			var account = await _context.CriptoAccounts.FirstOrDefaultAsync(x => x.DirectionUUID == UUID);

			return account;
		}


	}
}
