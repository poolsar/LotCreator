using System;

namespace ShopDataLib
{
    public class Context
    {
        private static ShopEntities _inst;

        public static ShopEntities Inst
        {
            get
            {
                if (_inst == null)
                {
                    _inst = new ShopEntities();
                }
                return _inst;
            }
        }

        public static void Save()
        {
            try
            {
                Inst.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            
        }



    }
}