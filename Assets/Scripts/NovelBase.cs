using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NovelBase : MonoBehaviour
{
    //CSVファイル用の変数
    TextAsset _csvFile;
    //CSVの文章を入れるためのリスト
    List<string[]> _csvDataList = new List<string[]>();
    //シーンに配置したTextオブジェクトを取得
    [SerializeField]
    private TextMeshProUGUI _nameText;
    //シーンに配置したText(TextMeshPro)オブジェクトを取得
    [SerializeField]
    private TextMeshProUGUI _messageText;
    //シナリオエンドフラッグ
    public bool ScenarioEndFlag = false;
    private int _rowsCount = 0;

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
    void Update()
    {
        //該当のシナリオが読み込み終わったらこれ以上処理を行わない
        if (ScenarioEndFlag)
        {
            return;
        }
        NameTextChange();
        MessageTextChange();
    }

    //名前のデータをTextに表示させる
    void NameTextChange()
    {
        //_csvDataListの行の先頭のデータを表示させる
        _nameText.text = _csvDataList[_rowsCount][0];
    }

    //メッセージのデータをText(TextMeshPro)に表示させる
    //何秒経過したか累積時間をカウントする変数
    public float CountTime = 0f;
    //次の文字を表示するまでに何秒かけるか
    public float NextTextTime = 0.3f;
    //実際に表示させる文字データ
    public string ViewStringData;
    //今、何文字目を表示しているのか判断するための値
    int MessageTextCount = 0;

    void MessageTextChange()
    {
        //メッセージテキストを一文字ずつ表示させる

        //_csvDataList[0][1]に入っているデータの長さ(Length)がMessageTextCountの数以上なら処理を止める
        if (_csvDataList[0][1].Length <= MessageTextCount)
        {
            return;
        }

        //一定時間(NextTextTime秒まで)経過したか判断する
        if (NextTextTime <= CountTime)
        {
            //経過時間をリセットする
            CountTime = 0f;

            //次の文字を表示する処理
            //_csvDataListの[行][列][データの先頭から何文字目を表示するか]
            ViewStringData += _csvDataList[0][1][MessageTextCount];

            //MessageTextCountの値を加算(次の文字を参照する)
            MessageTextCount++;

            //文字列を表示させる
            _messageText.text = ViewStringData;
        }

        //文字送り用に秒数をカウントする
        CountTime += Time.deltaTime;
    }
}
