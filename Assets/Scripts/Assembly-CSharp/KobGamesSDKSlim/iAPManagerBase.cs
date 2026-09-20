using System;
using System.Runtime.CompilerServices;
using KobGamesSDKSlim.iAP;
using UnityEngine;
using UnityEngine.Purchasing;

namespace KobGamesSDKSlim
{
	public class iAPManagerBase : Singleton<iAPManager>
	{
		public delegate void iAPSuccess(iAPProduct i_Product);

		public delegate void iAPFailed(iAPProduct i_Product, ePurchaseFailureReason i_Reason);

		protected bool m_IsRestore = true;

		private string m_CurrInappType;

		private static int s_TryAdddIAPListenerCount = 0;

		private static iAPSuccess m_OniAPPurchaseComplete = delegate { };
		private static iAPSuccess m_OniAPPurchaseRestoreComplete = delegate { };
		private static iAPFailed m_OniAPPurchaseFailed = delegate { };
		private static iAPFailed m_OniAPPurchaseRestoreFailed = delegate { };

		private const string k_IAPListenerType = "UnityEngine.Purchasing.IAPListener";

		private string m_AdsDetailsStatus => "UNITY_PURCHASING: True";

		public static event iAPSuccess OniAPPurchaseComplete
		{
			[CompilerGenerated]
			add
			{
				iAPSuccess current; iAPSuccess updated;
				do { current = m_OniAPPurchaseComplete; updated = (iAPSuccess)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OniAPPurchaseComplete, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				iAPSuccess current; iAPSuccess updated;
				do { current = m_OniAPPurchaseComplete; updated = (iAPSuccess)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OniAPPurchaseComplete, updated, current) != current);
			}
		}

		public static event iAPSuccess OniAPPurchaseRestoreComplete
		{
			[CompilerGenerated]
			add
			{
				iAPSuccess current; iAPSuccess updated;
				do { current = m_OniAPPurchaseRestoreComplete; updated = (iAPSuccess)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OniAPPurchaseRestoreComplete, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				iAPSuccess current; iAPSuccess updated;
				do { current = m_OniAPPurchaseRestoreComplete; updated = (iAPSuccess)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OniAPPurchaseRestoreComplete, updated, current) != current);
			}
		}

		public static event iAPFailed OniAPPurchaseFailed
		{
			[CompilerGenerated]
			add
			{
				iAPFailed current; iAPFailed updated;
				do { current = m_OniAPPurchaseFailed; updated = (iAPFailed)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OniAPPurchaseFailed, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				iAPFailed current; iAPFailed updated;
				do { current = m_OniAPPurchaseFailed; updated = (iAPFailed)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OniAPPurchaseFailed, updated, current) != current);
			}
		}

		public static event iAPFailed OniAPPurchaseRestoreFailed
		{
			[CompilerGenerated]
			add
			{
				iAPFailed current; iAPFailed updated;
				do { current = m_OniAPPurchaseRestoreFailed; updated = (iAPFailed)Delegate.Combine(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OniAPPurchaseRestoreFailed, updated, current) != current);
			}
			[CompilerGenerated]
			remove
			{
				iAPFailed current; iAPFailed updated;
				do { current = m_OniAPPurchaseRestoreFailed; updated = (iAPFailed)Delegate.Remove(current, value); } while (System.Threading.Interlocked.CompareExchange(ref m_OniAPPurchaseRestoreFailed, updated, current) != current);
			}
		}

		public static void DeleteIAPPlugins(bool i_IsForce = false)
		{
			return;
		}

		public void SetIsRestore(bool i_Value)
		{
			m_IsRestore = i_Value;
		}

		public void OnPurchaseComplete(Product i_Product)
		{
			if (i_Product == null)
				return;
			if (m_IsRestore)
				OnIAP_PurchaseRestoreComplete(i_Product);
			else
				OnIAP_PurchaseComplete(i_Product);
			ProcessPurchasedProduct(i_Product, m_IsRestore);
		}

		public void OnPurchaseFailed(Product i_Product, PurchaseFailureReason i_Reason)
		{
			if (m_IsRestore)
				OnIAP_PurchaseRestoreFailed(i_Product, i_Reason);
			else
				OnIAP_PurchaseFailed(i_Product, i_Reason);
		}

		protected virtual void OnIAP_PurchaseComplete(Product i_Product)
		{
			iAPSuccess callback = m_OniAPPurchaseComplete;
			if (callback == null) return;
			callback.Invoke(i_Product.ToiAPProduct().SetInappType(m_CurrInappType));
		}

		protected virtual void OnIAP_PurchaseRestoreComplete(Product i_Product)
		{
			iAPSuccess callback = m_OniAPPurchaseRestoreComplete;
			if (callback == null) return;
			callback.Invoke(i_Product.ToiAPProduct());
		}

		protected virtual void OnIAP_PurchaseFailed(Product i_Product, PurchaseFailureReason i_Reason)
		{
			iAPFailed callback = m_OniAPPurchaseFailed;
			if (callback == null) return;
			callback.Invoke(i_Product.ToiAPProduct().SetInappType(m_CurrInappType), (ePurchaseFailureReason)i_Reason);
		}

		protected virtual void OnIAP_PurchaseRestoreFailed(Product i_Product, PurchaseFailureReason i_Reason)
		{
			iAPFailed callback = m_OniAPPurchaseRestoreFailed;
			if (callback == null) return;
			callback.Invoke(i_Product.ToiAPProduct().SetInappType(m_CurrInappType), (ePurchaseFailureReason)i_Reason);
		}

		protected virtual void ProcessPurchasedProduct(Product i_Product, bool i_IsRestore)
		{
			return;
		}

		[Obsolete("Use the other method instead since Product Type is now needed.")]
		public void InitiatePurchase(string i_ProductId)
		{
			m_IsRestore = false;
			CodelessIAPStoreListener.Instance.InitiatePurchase(i_ProductId);
		}

		public void InitiatePurchase(string i_ProductId, string i_InappType)
		{
			m_CurrInappType = i_InappType;
			m_IsRestore = false;
			CodelessIAPStoreListener.Instance.InitiatePurchase(i_ProductId);
		}

		public void InititateRestore()
		{
			if (Application.platform != RuntimePlatform.IPhonePlayer &&
				Application.platform != RuntimePlatform.OSXPlayer &&
				Application.platform != RuntimePlatform.tvOS)
				return;
			m_IsRestore = true;
			CodelessIAPStoreListener listener = CodelessIAPStoreListener.Instance;
			if (listener == null) return;
			IAppleExtensions extensions = listener.GetStoreExtensions<IAppleExtensions>();
			extensions?.RestoreTransactions(success => { });
		}

		private void Reset()
		{
			return;
		}

		public void RemoveIAPListener()
		{
			s_TryAdddIAPListenerCount = 0;
			UnityEngine.Component listener = GetComponent("UnityEngine.Purchasing.IAPListener");
			if (listener != null)
				UnityEngine.Object.DestroyImmediate(listener, true);
		}

		public void AddIAPListener()
		{
			return;
		}
	}
}
