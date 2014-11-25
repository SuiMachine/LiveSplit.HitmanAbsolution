state("HMA")
{
	int time : "HMA.exe", 0x00E39278, 0x58;

}

start
{
	return current.time == 1;
}

split
{
}

isLoading
{
	return true;
}

gameTime
{
	return TimeSpan.FromSeconds(current.time);
}
