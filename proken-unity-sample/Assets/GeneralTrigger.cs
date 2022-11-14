using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// とてもシンプルな接地判定
// キャラクターの少し下に当たり判定(Trigger)をつける
public class GeneralTrigger : MonoBehaviour
{
    // フィールド
    // private なので、外部からアクセスできない
    // Trigger と重なっている物の個数を管理する
    private int judgeBuffer = 0;

    // プロパティ
    // public なので、外部からアクセスできて、フィールドのように振る舞う
    public bool judge { get{ return judgeBuffer != 0; } }

    // Start is called before the first frame update
    void Start()
    {
        // do nothing
    }

    // Update is called once per frame
    void Update()
    {
        // do nothing
    }

    // https://docs.unity3d.com/ja/2018.4/ScriptReference/Collision2D.html

    private void OnTriggerEnter2D(Collider2D other){
        judgeBuffer++;
    }

    private void OnTriggerStay2D(Collider2D other){
    }

    private void OnTriggerExit2D(Collider2D other){
        judgeBuffer--;
    }
}
