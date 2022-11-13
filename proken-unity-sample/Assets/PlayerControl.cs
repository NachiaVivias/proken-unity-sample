using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// プロジェクトにパッケージ InputSystem をインストールしないと使えません
// 新しいプロジェクトにインストールする手順：
//     - 右上の検索から 「Package Manager」 を見つけて開くとウィンドウが出てくる
//     - 「Packages : Unity Registry」の設定にしてリストから 「Input System」 を探す
//     - それをインストールする
//     - ダイアログが出てくるので Yes をクリックする
//     - エディタが再起動する
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{

    // フィールド（ class の中の変数のこと） に [SerializeField] をつけると、
    // このスクリプトをコンポーネントに付けたときにインスペクタからフィールドの値を操作できるようになる
    
    [SerializeField] public int jumpCooldown = 50;        // 次にジャンプ可能なのは何フレーム(1/50秒)後か
    [SerializeField] public float jumpVelocity = 500.0f;  // ジャンプの大きさ
    [SerializeField] public float walkVelocity = 6.0f;    // 左右移動の速さ

    [SerializeField] public GroundTrigger groundTrigger;  // スクリプト GroundTrigger を持つ GameObject を要求

    int jumpCooldownCounter = 0;

    Rigidbody2D rigidbodyMain;

    // Start は Update や FixedUpdate が定期的に呼ばれ始める前に 1 度だけ呼ばれる
    void Start()
    {
        rigidbodyMain = GetComponent<Rigidbody2D>();
    }

    // Update は毎フレーム呼ばれる
    void Update()
    {
        // キーボードの入力の現在の状態を取得
        var keyboard = Keyboard.current;

        // ジャンプができる状態のときに、スペースキーが押されていて、
        if (jumpCooldownCounter <= 0 && keyboard.spaceKey.isPressed){
            // 接地していれば、ジャンプ
            if(groundTrigger.judge){
                Debug.Log("spaceKey is pressed");
                rigidbodyMain.velocity = new Vector2(rigidbodyMain.velocity.x, 0.0f); // y 方向の速度を 0 にする
                rigidbodyMain.AddForce(new Vector2(0.0f, jumpVelocity)); // 上向きに飛ばす
                jumpCooldownCounter = jumpCooldown; // 次のジャンプは 1 秒後
            }
        }

        float xSpeed = 0.0f;

        if (keyboard.leftArrowKey.isPressed){
            xSpeed += -walkVelocity;
        }
        if (keyboard.rightArrowKey.isPressed){
            xSpeed += walkVelocity;
        }

        rigidbodyMain.velocity = new Vector2(xSpeed, rigidbodyMain.velocity.y);
    }

    // FixedUpdate は 1 秒間に 50 回のペースで呼ばれる
    void FixedUpdate(){
        jumpCooldownCounter--;
        if(jumpCooldownCounter < 0) jumpCooldownCounter = 0;
    }
}
