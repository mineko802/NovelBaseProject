using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class NovelBase : MonoBehaviour
{
    //CSVファイル用の変数
    TextAsset _csvFile;
    //CSVの文章を入れるためのリスト
    List<string[]> _csvDataList = new List<string[]>();

    [SerializeField]
    private int _messageCount = 0;
    private int _rowsCount = 0;
    private float _countTime = 0f;
    [Header("メッセージの文字送り時間を変更する変数")]
    [SerializeField]
    private float _messageStopTime = 0.1f;
    [Header("名前表示用のTextMeshProUGUIを入れる変数")]
    [SerializeField]
    private TextMeshProUGUI _nameText;

    [Header("メッセージ表示用のTextMeshProUGUIを入れる変数")]
    //キャラクターのセリフを表示させるTextボックス
    [SerializeField]
    private TextMeshProUGUI _messageText;
    [Header("選択肢を表示させるPrefabを入れる変数")]
    //選択肢表示用のオブジェクトを入れる変数
    [SerializeField]
    private GameObject _selectCommandObject;

    //メッセージを止めるためのフラグ
    private bool _messageStopFlag = false;

    //コマンドチェックフラグ
    private bool _commandCheckFlag = false;

    private List<string[]> _getSelectCommand = new List<string[]>();

    [Header("シナリオが終了したかの判定を行うフラグ")]
    //シナリオエンドフラッグ
    public bool ScenarioEndFlag = false;

    void Start()
    {
        //_csvFileの中にResourcesフォルダの中にTestCSVという名前のファイルを入れる
        _csvFile = Resources.Load("TestCSV") as TextAsset;
        //_csvFileの中にあるデータをString形式に変換
        StringReader reader = new StringReader(_csvFile.text);

        //リストに追加していく
        while (reader.Peek() != -1)//reader.Peekが-1になるまで
        {
            string line = reader.ReadLine();//一行ずつ読み込む
            _csvDataList.Add(line.Split(','));// , 区切りでリストに追加
        }
    }

    // Update is called once per frame
    private void Update()
    {
        //該当のシナリオが読み込み終わったらこれ以上処理を行わない
        if (ScenarioEndFlag)
        {
            return;
        }

        if (_commandCheckFlag == false)
        {
            GetCommand();
        }

        //メッセージ表示が完全に終了しているかつクリックされたら次のテキストに変更する
        //表示テキスト以上になった場合は表示処理を止める
        if (_rowsCount >= _csvDataList.Count || _messageCount >= _csvDataList[_rowsCount][2].Length || _messageStopFlag)
        {
            return;
        }

        //名前の表示処理を呼び出す
        NameTextView();

        //メッセージ表示処理を呼び出す
        MessageTextView();
    }

    private void NameTextView()
    {
        _nameText.text = _csvDataList[_rowsCount][1];
    }

    private void MessageTextView()
    {
        //一文字ずつ文字を表示させる
        if (_messageStopTime <= _countTime)
        {
            _countTime = 0f;
            _messageText.text += _csvDataList[_rowsCount][2][_messageCount];
            _messageCount++;
        }

        //文字送り用に秒数を数える
        _countTime += Time.deltaTime;
    }

    private void GetCommand()
    {
        //もしもコマンドがあった場合、選択肢表示をする
        string commandCheck = _csvDataList[_rowsCount][0];

        //コマンドが入力されているか確認する
        switch (commandCheck)
        {
            //選択肢を2つ表示させる

            case "Select1":
                SelectCommand(1);
                break;

            case "Select2":
                SelectCommand(2);
                break;

            case "Select3":
                SelectCommand(3);
                break;

            case "Select4":
                SelectCommand(4);
                break;

            case "Select5":
                SelectCommand(5);
                break;

            //指定のシナリオまで飛ぶ
            case "JumpCommand":
                JumpMessageRow(_csvDataList[_rowsCount][1]);
                break;

            //シナリオが終わったら処理を止める
            case "Scenario_End":
                ScenarioEndFlag = true;
                break;

            //もしコマンドじゃなかったらリセットする
            default:
                //メッセージテキストの表示を空白に変更する
                _messageText.text = "";
                break;
        }
        //コマンドの確認を終えたのでFlagをtrueにする
        _commandCheckFlag = true;
    }

    private void SelectCommand(int SelectCommandValue)
    {
        //SelectCommandObjectの子オブジェクトの選択肢を引数nつを取得して表示させる
        //メッセージがこれ以上流れないようにフラグをtrueに変更する
        _messageStopFlag = true;

        _getSelectCommand.Clear();

        //子オブジェクトを一度全て非表示にする
        foreach (Transform child in _selectCommandObject.transform)
        {
            child.gameObject.SetActive(false);
        }

        //指定の数の子オブジェクトを取得する
        for (var i = 0; i < SelectCommandValue; ++i)
        {
            //一つ下の段を見る
            _rowsCount++;
            var selectObject = _selectCommandObject.transform.GetChild(i);
            selectObject.GetChild(0).gameObject.GetComponent<TextMeshProUGUI>().text = _csvDataList[_rowsCount][1];
            _getSelectCommand.Add(_csvDataList[_rowsCount]);
            selectObject.gameObject.SetActive(true);
        }
        //表示させた
        _selectCommandObject.SetActive(true);
    }

    private void JumpMessageRow(string SelectCommandValue)
    {
        //JumpCommandの飛び先を探したいので_Startを付けた文字列で検索する
        var jumpCommand = SelectCommandValue + "_Start";

        //JumpCommand_A
        for (var i = 0; i < _csvDataList.Count; ++i)
        {
            if (_csvDataList[i][0] == jumpCommand)
            {
                _rowsCount = i;
            }
        }
        _rowsCount += 1;
        Debug.Log(_rowsCount);
        _messageStopFlag = false;
        CommandClear();
    }


    private void CommandClear()
    {
        _commandCheckFlag = false;
        _selectCommandObject.SetActive(false);
        _messageText.text = "";
    }

    public void NextMessageView()
    {
        //もし文字が表示と途中なら最後まで表示させる
        if (_messageCount < _csvDataList[_rowsCount][2].Length)
        {
            _messageText.text = _csvDataList[_rowsCount][2];
            _messageCount = _csvDataList[_rowsCount][2].Length;
            return;
        }

        //読みこんだメッセージの行以下ならカウントを追加する
        if (_rowsCount >= _csvDataList.Count || _messageStopFlag)
        {
            return;
        }
        //次の行に移る
        _rowsCount++;
        //Messageを先頭から出す
        _messageCount = 0;
        //次のフラグを確認する
        _commandCheckFlag = false;
    }

    public void SelectCommandReturnValue(int SelectCommandValue)
    {
        Debug.Log(_getSelectCommand[SelectCommandValue][3]);
        //押されたボタンの位置を情報として表示する
        //押されたボタンの位置を情報として渡す
        JumpMessageRow(_getSelectCommand[SelectCommandValue][3]);
    }
}
