using System;
using System.Threading;

public static class ReadWriteLockSlimExtend
{
	public static void PerformUsingReadLock(this ReaderWriterLockSlim readerWriterLockSlim, Action action)
	{
		readerWriterLockSlim.EnterReadLock();
		try { action(); }
		finally { readerWriterLockSlim.ExitReadLock(); }
	}

	public static T PerformUsingReadLock<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> action)
	{
		readerWriterLockSlim.EnterReadLock();
		try { return action(); }
		finally { readerWriterLockSlim.ExitReadLock(); }
	}

	public static void PerformUsingWriteLock(this ReaderWriterLockSlim readerWriterLockSlim, Action action)
	{
		readerWriterLockSlim.EnterWriteLock();
		try { action(); }
		finally { readerWriterLockSlim.ExitWriteLock(); }
	}

	public static T PerformUsingWriteLock<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> action)
	{
		readerWriterLockSlim.EnterWriteLock();
		try { return action(); }
		finally { readerWriterLockSlim.ExitWriteLock(); }
	}

	public static void PerformUsingUpgradeableReadLock(this ReaderWriterLockSlim readerWriterLockSlim, Action action)
	{
		readerWriterLockSlim.EnterUpgradeableReadLock();
		try { action(); }
		finally { readerWriterLockSlim.ExitUpgradeableReadLock(); }
	}

	public static T PerformUsingUpgradeableReadLock<T>(this ReaderWriterLockSlim readerWriterLockSlim, Func<T> action)
	{
		readerWriterLockSlim.EnterUpgradeableReadLock();
		try { return action(); }
		finally { readerWriterLockSlim.ExitUpgradeableReadLock(); }
	}
}
