using HitIt.Other;
using HitIt.Storage;

namespace HitIt.Ecs
{
    public class KnifeUIHandler : IInizializable
    {
        private KnifeUIMono knifeUI;

        public void Inizialize()
        {
            knifeUI = StorageFacility.Instance.GetInterface(InterfaceObject.KnifeUI).GetComponent<KnifeUIMono>();
        }

        public void SetKnifeAmount(int curr, int total)
        {
            knifeUI.SetKnifesAmount(curr, total);
        }
    }
}