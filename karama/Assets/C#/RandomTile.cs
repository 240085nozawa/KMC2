using System.Collections.Generic;
using UnityEngine;

public class RandomTile : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    // �F�t���^�C���p�̃}�e���A����Inspector���犄�蓖��
    public float changeInterval = 2f; // �F�̕ω��Ԋu
    public LayerMask tileLayer; // �^�C������p���C���[�iPlayer�Ɠ����j

    private Renderer tileRenderer;

    // �}�e���A���ꗗ�i�S�F���������Ń}�b�s���O�j
    public List<Material> allMaterials;

    // �J�n���ɏ�����
    void Start()
    {
        tileRenderer = GetComponent<Renderer>();

        if (allMaterials == null || allMaterials.Count == 0)
        {
            Debug.LogError("[RandomTile] �}�e���A�����ݒ肳��Ă��܂���I");
            enabled = false;
            return;
        }

        InvokeRepeating(nameof(ChangeColorFromNeighbors), 1f, changeInterval);
    }

    // ���͂̃^�C���̐F���擾���A�������烉���_���ŕω�
    void ChangeColorFromNeighbors()
    {
        List<string> neighborTextureNames = new List<string>();

        Vector3[] directions = {
            Vector3.forward,
            Vector3.back,
            Vector3.left,
            Vector3.right
        };

        foreach (Vector3 dir in directions)
        {
            Vector3 checkPos = transform.position + dir;
            Collider[] hits = Physics.OverlapSphere(checkPos, 0.4f, tileLayer);

            foreach (var hit in hits)
            {
                Renderer neighborRenderer = hit.GetComponent<Renderer>();
                if (neighborRenderer != null && neighborRenderer.material.mainTexture != null)
                {
                    string texName = neighborRenderer.material.mainTexture.name;
                    if (!neighborTextureNames.Contains(texName))
                    {
                        neighborTextureNames.Add(texName);
                    }
                }
            }
        }

        if (neighborTextureNames.Count == 0) return;

        // ���݂̐F
        string currentTex = tileRenderer.material.mainTexture.name;
        int currentIndex = neighborTextureNames.IndexOf(currentTex);

        // ���̃C���f�b�N�X�i�Ō�Ȃ�0�ɖ߂�j
        int nextIndex = (currentIndex + 1) % neighborTextureNames.Count;
        string nextTexName = neighborTextureNames[nextIndex];

        // ��v����}�e���A����T���ēK�p
        Material nextMat = allMaterials.Find(mat => mat.mainTexture != null && mat.mainTexture.name == nextTexName);

        if (nextMat != null)
        {
            tileRenderer.material = nextMat;
            Debug.Log($"[RandomTile] �F�ύX �� {nextMat.name}");
        }

    }
}
