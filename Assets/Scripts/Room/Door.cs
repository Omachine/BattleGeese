using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private static Player _player;
    private Room _room;
    public Door NextDoor;
    private const float _doorTime = 1f;
    private float _timeElapsedIn;
    private GameObject _visualArea;
    private ParticleSystem _doorAura;

    private static Collider[] _playerIn = new Collider[3];

    public bool _allPlayersIn = false;

    private Animator _animator;

    // Hitbox
    private Vector3 _hitboxPos;
    private Vector3 _hitboxSize;

    private static CoolAnimationClass _animationClass;

    // New fields
    [SerializeField] private bool isLobbyRoom = false;
    [SerializeField] private string NextGameScene;

    private void Awake()
    {
        _animator = transform.Find("Sprite").GetComponent<Animator>();
        _visualArea = transform.Find("Area").gameObject;
        
        _doorAura = transform.Find("Aura").GetComponent<ParticleSystem>();

        _player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        _animationClass = new CoolAnimationClass();
        // Hitbox
        _hitboxPos = transform.position + new Vector3(0f, 1f, 0f);
        _hitboxSize = Vector3.one;

        _room = GetComponentInParent<Room>();
        if (!NextDoor)
        {
            Debug.Log("I don't have a pair!", gameObject);
        }

        _room.OnLock += OnRoomLock;
        // if (!_room._firstRoom) _room.Deactivate();
    }

    private void OnTriggerEnter(Collider other)
    {
        // TODO change the 1 to the quantity of players in-game
        if (PlayersIn() == 1)
        {
            _allPlayersIn = true;
            _timeElapsedIn = 0;
        }
    }

    private void OnTriggerExit(Collider other) => _allPlayersIn = false;

    private void Update()
    {
        if (_allPlayersIn && !_room.isInBattle) _timeElapsedIn += Time.deltaTime;

        if (_timeElapsedIn < _doorTime) return;

        ExitRoom();
    }

    private int PlayersIn()
    {
        return Physics.OverlapBoxNonAlloc(_hitboxPos, _hitboxSize, _playerIn, quaternion.identity, LayerMask.GetMask("Player"));
    }

    private void EnterRoom()
    {
        _room.Activate();

        // TP all player to door
        Vector3 spawnpoint = transform.position + transform.forward * 1.5f;

        // keep player y coord
        spawnpoint.y = _player.transform.position.y;

        foreach (var player in _playerIn)
        {
            if (!player) continue;
            player.gameObject.GetComponent<Rigidbody>().position = spawnpoint;
        }

        _timeElapsedIn = 0f;

        _animator.SetBool("opened", false);
    }

    private void ExitRoom()
    {
        _timeElapsedIn = 0f;
        _allPlayersIn = false;

        _animator.SetBool("opened", true);

        _animationClass.CoolRoomTransition(this);
        _player.StateMachine.EnteringDoorState.SetDoor(this);
        _player.StateMachine.ChangeState(_player.StateMachine.EnteringDoorState);
    }

    public void GoToNextDoor()
    {
        if (isLobbyRoom)
        {
            SavePlayer.instance.ChangeScene(NextGameScene);
            // SceneManager.LoadScene(NextGameScene);
        }
        _animationClass.CoolRoomTransitionExit();
        _room.Deactivate();
        if (!isLobbyRoom) NextDoor.EnterRoom();
    }

    private void OnDrawGizmos()
    {
        // Hitbox
        Vector3 pos = transform.position + new Vector3(0f, 1f, 0f);
        Vector3 size = 2f * Vector3.one;
        Gizmos.DrawWireCube(pos, size);
    }

    public void StartRoomBattle() => _room.StartBattle();

    void OnRoomLock(bool isLocked)
    {
        _animator.SetBool("locked", isLocked);
        _visualArea.SetActive(!isLocked);
        
        if (isLocked)
        {
            _doorAura.Stop();
            _doorAura.Clear();
        }
        else _doorAura.Play();
    }
}
