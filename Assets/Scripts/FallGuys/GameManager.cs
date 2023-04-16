using UnityEngine;
using Fusion; // Photon Fusion의 기능을 쓰기 위한 네임스페이스

// 접근할 네임스페이스 'Yukgaejang.FallGuys'로 지정
namespace Yukgaejang.FallGuys
{
    public class GameManager : NetworkBehaviour, IStateAuthorityChanged
    {
        public void StateAuthorityChanged()
        {
            throw new System.NotImplementedException();
        }
    }
}