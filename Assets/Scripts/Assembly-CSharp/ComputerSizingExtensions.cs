public static class ComputerSizingExtensions
{
	private const int INT_OneKB = 1024;

	public static int KB(this int value) => value << 10;

	public static int MB(this int value) => value << 20;

	public static int GB(this int value) => value << 30;

	public static int TB(this int value) => 0;

	public static int PB(this int value) => 0;

	public static int EB(this int value) => 0;

	public static int ZB(this int value) => 0;

	public static int YB(this int value) => 0;
}
