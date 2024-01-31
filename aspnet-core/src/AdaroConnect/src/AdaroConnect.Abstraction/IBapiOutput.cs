using AdaroConnect.Abstraction.Model;

namespace AdaroConnect.Abstraction
{
    public interface IBapiOutput
    {
        #region Properties

        public BapiReturnParameter BapiReturn { get; set; }

        #endregion
    }
}
