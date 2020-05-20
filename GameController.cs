using UnityEngine;
using UnityEngine.UI;
using System.Collections;


[System.Serializable]
public class Player {
   public Image panel;
   public Text text;
   public Button button;
}

[System.Serializable]
public class PlayerColor {
   public Color panelColor;
   public Color textColor;
}

public class GameController : MonoBehaviour {

    public Text[] buttonList;
	public Text gameOverText;
	public GameObject gameOverPanel;
	private int playerno;
	private int moveCount=0;
	public bool ends=false;
	public GameObject RestartButton;
	public Player playerX;
	public Player playerO;
	public PlayerColor activePlayerColor;
	public PlayerColor inactivePlayerColor;
	public GameObject startInfo;
	public Text startInfotext;
	
	private string[] playerSide={"X","O"};
	
	public void SetPlayerButton(bool toggle){
		playerX.button.interactable=toggle;
		playerO.button.interactable=toggle;
	}
	
	public void SetPlayerColors(Player newPlayer,Player oldPlayer){
		newPlayer.panel.color = activePlayerColor.panelColor;
		newPlayer.text.color = activePlayerColor.textColor;
		oldPlayer.panel.color = inactivePlayerColor.panelColor;
		oldPlayer.text.color = inactivePlayerColor.textColor;	
	}
	
	public void SetPlayerColorswin(Player winPlayer,Player lossePlayer){
		winPlayer.panel.color = activePlayerColor.panelColor;
		winPlayer.text.color = activePlayerColor.textColor;
		lossePlayer.panel.color = inactivePlayerColor.panelColor;
		lossePlayer.text.color = inactivePlayerColor.textColor;	
	}
	
	public void SetPlayerColorsdraw(Player winPlayer,Player lossePlayer){
		winPlayer.panel.color = inactivePlayerColor.panelColor;
		winPlayer.text.color = inactivePlayerColor.textColor;
		lossePlayer.panel.color = inactivePlayerColor.panelColor;
		lossePlayer.text.color = inactivePlayerColor.textColor;	
	}
	
	public void SetStartingSide(string startingside){
		if (startingside=="X"){
			SetPlayerColors(playerX,playerO);
			playerno=1;
		}
		else{
			playerno=0;
			SetPlayerColors(playerO,playerX);
		}
		StartGame();
	}
	
	void StartGame(){
		SetBoardInteractable (true);
		SetPlayerButton(false);
		ends=false;
		startInfotext.text="Game ON!!";
	}
	
	public void restartGame(){
		moveCount=0;
		SetPlayerColorsdraw(playerX,playerO);
		SetPlayerButton(true);
		for (int i = 0; i < buttonList.Length; i++)
        {
        	buttonList[i].text="";
		}
        gameOverPanel.SetActive(false);
		RestartButton.SetActive(false);
		startInfo.SetActive(true);
		startInfotext.text="'X' OR 'O''? \n Choose a Side";
		
	}
	
	void SetBoardInteractable (bool toggle) {
		for (int i = 0; i < buttonList.Length; i++)
        {
            buttonList[i].GetComponentInParent<Button>().interactable = toggle;
        }
	}

    void SetGameOverText(string x){
    	gameOverText.text=x;
	}
	
    void Awake ()
    {
        SetGameControllerReferenceOnButtons();
	gameOverPanel.SetActive(false);
	RestartButton.SetActive(false);
    }

    void SetGameControllerReferenceOnButtons ()
    {
        for (int i = 0; i < buttonList.Length; i++)
        {            buttonList[i].GetComponentInParent<GridSpace>().SetGameControllerReference(this);
        }
    }

    public string GetPlayerSide ()
    {
    	if (playerno==0){
    		playerno=1;
		}
		else{
			playerno=0;
		}
		if (playerno == 1) {
			SetPlayerColors(playerX,playerO);
			} 
		else {
			SetPlayerColors(playerO,playerX);
			}
        return playerSide[playerno];
    }

        public void EndTurn ()
    {
    	moveCount++;
        if (buttonList [0].text ==buttonList [1].text && buttonList [1].text ==buttonList [2].text && buttonList [0].text!="")
        {
            GameOver(playerSide[playerno]);
        }
        if (buttonList [3].text ==buttonList [4].text && buttonList [4].text ==buttonList [5].text && buttonList [3].text!="")
        {
            GameOver(playerSide[playerno]);
        }
        if (buttonList [6].text ==buttonList [7].text && buttonList [7].text ==buttonList [8].text && buttonList [6].text!="")
        {
            GameOver(playerSide[playerno]);
        }
        if (buttonList [0].text ==buttonList [4].text && buttonList [4].text ==buttonList [8].text && buttonList [0].text!="")
        {
            GameOver(playerSide[playerno]);
        }
        if (buttonList [2].text ==buttonList [4].text && buttonList [4].text ==buttonList [6].text && buttonList [2].text!="")
        {
            GameOver(playerSide[playerno]);
        }
        if (buttonList [2].text ==buttonList [5].text && buttonList [5].text ==buttonList [8].text && buttonList [2].text!="")
        {
            GameOver(playerSide[playerno]);
        }
        if (buttonList [0].text ==buttonList [3].text && buttonList [3].text ==buttonList [6].text && buttonList [0].text!="")
        {
            GameOver(playerSide[playerno]);
        }
        if (buttonList [1].text ==buttonList [4].text && buttonList [4].text ==buttonList [7].text && buttonList [1].text!="")
        {
            GameOver(playerSide[playerno]);
        }
        if (moveCount >= 9 && !(ends)) { gameOverPanel.SetActive(true); GameOver("draw");  }
    }

    void GameOver (string winningplayer)	
    {
        SetBoardInteractable (false);
        ends=true;
		gameOverPanel.SetActive(true);
		if (winningplayer=="draw"){
			SetGameOverText("It's a draw!");
		}
		else{
			SetGameOverText(playerSide[playerno] + " Wins!");
		}
		if (winningplayer=="draw"){
			SetPlayerColorsdraw(playerX,playerO);
		}
		else{
			if (playerSide[playerno]=="X"){
				SetPlayerColorswin(playerX,playerO);
			}
			else{
				SetPlayerColorswin(playerO,playerX);
			}
		}
		startInfo.SetActive(false);	
        RestartButton.SetActive(true);
    }
}