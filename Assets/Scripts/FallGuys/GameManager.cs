using UnityEngine;
using Fusion; // Photon Fusion�� ����� ���� ���� ���ӽ����̽�

// ������ ���ӽ����̽� 'Yukgaejang.FallGuys'�� ����
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