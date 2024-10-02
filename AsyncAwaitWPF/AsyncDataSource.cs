namespace AsyncAwaitWPF;

public class AsyncDataSource
{
	public async IAsyncEnumerable<int> GetNumbers()
	{
		while (true)
		{
			await Task.Delay(Random.Shared.Next() % 1000 + 500); //Warten zw. 0.5s und 1.5s
			yield return Random.Shared.Next();
		}

		//List<int> numbers = [];
		//numbers.Add(Random.Shared.Next());
		//return numbers;
	}
}