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
    Rigidbody2D rigidbodyMain;
    int jumpCooldown = 0; // 次にジャンプ可能なのは何フレーム(1/50秒)後か

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

        // ジャンプができる状態のときに、スペースキーが押されているとジャンプ
        if (jumpCooldown <= 0 && keyboard.spaceKey.isPressed){
            Debug.Log("spaceKey is pressed");
            rigidbodyMain.AddForce(new Vector2(0.0f, 500.0f)); // 上向きに飛ばす
            jumpCooldown = 50; // 次のジャンプは 1 秒後
        }

        float xSpeed = 0.0f;

        if (keyboard.leftArrowKey.isPressed){
            xSpeed += -6.0f;
        }
        if (keyboard.rightArrowKey.isPressed){
            xSpeed += 6.0f;
        }

        rigidbodyMain.velocity = new Vector2(xSpeed, rigidbodyMain.velocity.y);
    }

    // FixedUpdate は 1 秒間に 50 回のペースで呼ばれる
    void FixedUpdate(){
        jumpCooldown--;
        if(jumpCooldown < 0) jumpCooldown = 0;
    }
}
