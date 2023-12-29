using System.Collections;
using TMPro;
using UnityEngine;

// 游戏管理器
public class GameManager : MonoBehaviour
{
    public TileBoard board; // 游戏区域 
    public CanvasGroup gameOver; // 游戏结束组件
    public TextMeshProUGUI scoreText; // 分数组件
    public TextMeshProUGUI hiscoreText; // 最高分数组件

    private int score; // 当前分数

    private void Start()
    {
        NewGame();
    }

    /// <summary>
    /// 开启一个新游戏
    /// </summary>
    private void NewGame()
    {
        // 重置分数
        SetScore(0);
        // 设置最高分数
        hiscoreText.text = LoadHiscore().ToString();
        // 游戏结束淡出
        gameOver.alpha = 0f;
        // 允许交互
        gameOver.interactable = false;
        // 清除游戏区域状态
        board.ClearBoard();
        // 生成初始tile
        board.CreateTile();
        board.CreateTile();
        // 允许操作游戏区域
        board.enabled = true;
    }

    /// <summary>
    /// 游戏结束
    /// </summary>
    public void GameOver()
    {
        // 不允许操作游戏区域
        board.enabled = false;
        // 允许游戏结束组件交互，启用组件
        gameOver.interactable = true;
        // 开始游戏结束动画
        StartCoroutine(Fade(gameOver, 1f, 1f));
    }

    /// <summary>
    /// 使用动画处理canvas
    /// </summary>
    /// <param name="canvasGroup"></param>
    /// <param name="to"></param>
    /// <param name="delay"></param>
    /// <returns></returns>
    private IEnumerator Fade(CanvasGroup canvasGroup, float to, float delay)
    {
        // 等待delay时间
        yield return new WaitForSeconds(delay);

        float elapsed = 0f;
        float duration = 0f;
        float from = canvasGroup.alpha;

        // 当elapsed小于duration时，绘制过程
        while (elapsed < duration)
        {
            canvasGroup.alpha = Mathf.Lerp(from, to, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        canvasGroup.alpha = to;
    }


    /// <summary>
    /// 增加分数
    /// </summary>
    /// <param name="points"></param>
    public void IncreaseScore(int points)
    {
        SetScore(score + points);
    }

    /// <summary>
    /// 设置分数，设置分数组件文字，保存最高分数
    /// </summary>
    /// <param name="score"></param>
    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString();
        SaveHiscore();
    }

    /// <summary>
    /// 保存分数最高时间
    /// </summary>
    private void SaveHiscore()
    {
        int hiscore = LoadHiscore();
        if (score > hiscore)
        {
            PlayerPrefs.SetInt("hiscore", score);
        }
    }

    /// <summary>
    /// 载入最高分数
    /// </summary>
    /// <returns></returns>
    private int LoadHiscore()
    {
        return PlayerPrefs.GetInt("hiscore", 0);
    }
}