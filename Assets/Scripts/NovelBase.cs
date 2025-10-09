using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NovelBase : MonoBehaviour
{
    //CSV�t�@�C���p�̕ϐ�
    TextAsset _csvFile;
    //CSV�̕��͂����邽�߂̃��X�g
    List<string[]> _csvDataList = new List<string[]>();
    //�V�[���ɔz�u����Text�I�u�W�F�N�g���擾
    public Text NameText;
    //�V�[���ɔz�u����Text(TextMeshPro)�I�u�W�F�N�g���擾
    public TextMeshProUGUI MessageText;

    void Start()
    {
        //_csvFile�̒���Resources�t�H���_�̒���TestCSV�Ƃ������O�̃t�@�C��������
        _csvFile = Resources.Load("TestCSV") as TextAsset;
        //_csvFile�̒��ɂ���f�[�^��String�`���ɕϊ�
        StringReader reader = new StringReader(_csvFile.text);

        //���X�g�ɒǉ����Ă���
        while (reader.Peek() != -1)//reader.Peek��-1�ɂȂ�܂�
        {
            string line = reader.ReadLine();//��s���ǂݍ���
            _csvDataList.Add(line.Split(','));// , ��؂�Ń��X�g�ɒǉ�
        }

        //�ǂݍ���CSV�f�[�^��_csvDataList[��][�s]�ŏo�͂ł���

        //CSV�f�[�^��A1�̃Z���̒��ɓ����Ă���f�[�^���o��
        Debug.Log(_csvDataList[1][0]);
        //CSV�f�[�^�̂a1�̃Z���̒��ɓ����Ă���f�[�^���o��
        Debug.Log(_csvDataList[1][1]);
    }

    // Update is called once per frame
    void Update()
    {
        NameTextChange();
        MessageTextChange();
    }

    //���O�̃f�[�^��Text�ɕ\��������
    void NameTextChange()
    {
        //_csvDataList�̍s�̐擪�̃f�[�^��\��������
        NameText.text = _csvDataList[0][0];
    }

    //���b�Z�[�W�̃f�[�^��Text(TextMeshPro)�ɕ\��������
    //���b�o�߂������ݐώ��Ԃ��J�E���g����ϐ�
    public float CountTime = 0f;
    //���̕�����\������܂łɉ��b�����邩
    public float NextTextTime = 0.3f;
    //���ۂɕ\�������镶���f�[�^
    public string ViewStringData;
    //���A�������ڂ�\�����Ă���̂����f���邽�߂̒l
    int MessageTextCount = 0;

    void MessageTextChange()
    {
        //���b�Z�[�W�e�L�X�g���ꕶ�����\��������

        //_csvDataList[0][1]�ɓ����Ă���f�[�^�̒���(Length)��MessageTextCount�̐��ȏ�Ȃ珈�����~�߂�
        if (_csvDataList[0][1].Length <= MessageTextCount)
        {
            return;
        }

        //��莞��(NextTextTime�b�܂�)�o�߂��������f����
        if (NextTextTime <= CountTime)
        {
            //�o�ߎ��Ԃ����Z�b�g����
            CountTime = 0f;

            //���̕�����\�����鏈��
            //_csvDataList��[�s][��][�f�[�^�̐擪���牽�����ڂ�\�����邩]
            ViewStringData += _csvDataList[0][1][MessageTextCount];

            //MessageTextCount�̒l�����Z(���̕������Q�Ƃ���)
            MessageTextCount++;

            //�������\��������
            MessageText.text = ViewStringData;
        }

        //��������p�ɕb�����J�E���g����
        CountTime += Time.deltaTime;
    }
}
