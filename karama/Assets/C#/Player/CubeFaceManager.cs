using UnityEngine;
using System.Collections.Generic;

// �L���[�u�̊e�ʂ̐F��ԂƉ�]�������Ǘ�����N���X

public class CubeFaceManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Dictionary<string, string> faceTextures = new Dictionary<string, string>
    {
        { "Top", "PurpleTairu" },
        { "Bottom", "BlueTairu" },
        { "Front", "RedTairu" },
        { "Back", "GreenTairu" },
        { "Left", "BlackTairu" },
        { "Right", "YellowTairu" }
    };

    // �w��ʂ̐F���擾
    public string GetFace(string faceName)
    {
        return faceTextures.ContainsKey(faceName) ? faceTextures[faceName] : "";
    }

    // �i�s�����ɉ����Ėʂ̐F�����X�V�i��]�j
    public void Rotate(Vector3 direction)
    {
        string temp;

        if (direction == Vector3.forward)
        {
            temp = faceTextures["Top"];
            faceTextures["Top"] = faceTextures["Back"];
            faceTextures["Back"] = faceTextures["Bottom"];
            faceTextures["Bottom"] = faceTextures["Front"];
            faceTextures["Front"] = temp;
        }
        else if (direction == Vector3.back)
        {
            temp = faceTextures["Top"];
            faceTextures["Top"] = faceTextures["Front"];
            faceTextures["Front"] = faceTextures["Bottom"];
            faceTextures["Bottom"] = faceTextures["Back"];
            faceTextures["Back"] = temp;
        }
        else if (direction == Vector3.right)
        {
            temp = faceTextures["Top"];
            faceTextures["Top"] = faceTextures["Left"];
            faceTextures["Left"] = faceTextures["Bottom"];
            faceTextures["Bottom"] = faceTextures["Right"];
            faceTextures["Right"] = temp;
        }
        else if (direction == Vector3.left)
        {
            temp = faceTextures["Top"];
            faceTextures["Top"] = faceTextures["Right"];
            faceTextures["Right"] = faceTextures["Bottom"];
            faceTextures["Bottom"] = faceTextures["Left"];
            faceTextures["Left"] = temp;
        }
    }

    // �i�s�����ɑΉ�����ʂ̐F�i�^�C���ƈ�v����p�j
    public string GetFaceForDirection(Vector3 direction)
    {
        if (direction == Vector3.forward) return faceTextures["Front"];
        if (direction == Vector3.back) return faceTextures["Back"];
        if (direction == Vector3.right) return faceTextures["Right"];
        if (direction == Vector3.left) return faceTextures["Left"];
        return "";
    }
}
