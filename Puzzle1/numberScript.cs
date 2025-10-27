public void PressNumber(int number)
{
    
    currentInput += number.ToString();

    
    RefreshDisplay();

   
    if (!correctInput.StartsWith(currentInput))
    {
        
        ShowError();
        RefreshInput();
        return; 
    }

    
    if (currentInput.Length == correctInput.Length)
    {
        
        ShowSucess();
        
    }
    
}

private void RefreshInput()
{
    currentInput = "";
    RefreshDisplay();
}