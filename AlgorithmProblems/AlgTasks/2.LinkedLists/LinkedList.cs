using System;
using System.Collections.Generic;

namespace AlgTasks.LinkedLists
{
	public static class LinkedListExtension
	{
		/// <summary>
		/// Remove duplicates from list
		/// </summary>
		/// <typeparam name="T">Type of list's content</typeparam>
		/// <param name="list">Linked list</param>
		public static void RemoveDuplicates<T>(this Helpers.LinkedList<T> list)
		{
			if (list == null || list.IsEmpty)
				return;

			var valuesHash = new HashSet<T>();
			var previous = list.Head;
			var currentListItem = list.Head;

			while (currentListItem != null)
			{
				if (valuesHash.Contains(currentListItem.Content))
				{
					previous.Next = currentListItem.Next;
				}
				else
				{
					valuesHash.Add(currentListItem.Content);
					previous = currentListItem;
				}
				currentListItem = currentListItem.Next;
			}
		}

		/// <summary>
		/// Get k-th element from end of linked list (based 0)
		/// </summary>
		/// <typeparam name="T">Type of list's content</typeparam>
		/// <param name="list">Linked list</param>
		/// <returns>K-th element from end or null if length of list is less</returns>
		public static Helpers.LinkedListNode<T> GetKFromEnd<T>(this Helpers.LinkedList<T> list, int k)
		{
			if (list == null || list.IsEmpty)
			{
				return null;
			}

			var secondRunner = list.Head;
			var currentPosition = 0;
			while (secondRunner?.Next != null && currentPosition < k)
			{
				secondRunner = secondRunner.Next;
				currentPosition++;
			}

			if (currentPosition < k)
			{
				return null;
			}

			var firstRunner = list.Head;
			while (secondRunner.Next != null)
			{
				secondRunner = secondRunner.Next;
				firstRunner = firstRunner.Next;
			}

			return firstRunner;
		}

		/// <summary>
		/// Sort linked list: all items less X should be in left part, and greater or equals - in right part
		/// </summary>
		/// <typeparam name="T">Type of list's content</typeparam>
		/// <param name="list">Linked list</param>
		/// <param name="content">Content to be X</param>
		public static void SortAround<T>(this Helpers.LinkedList<T> list, T content) where T : IComparable<T>
		{
			if (list == null || list.IsEmpty)
			{
				return;
			}

			Helpers.LinkedListNode<T> listBeforeHead = null;
			Helpers.LinkedListNode<T> listAfterHead = null;
			Helpers.LinkedListNode<T> currentBefore = null;
			Helpers.LinkedListNode<T> currentAfter = null;

			var current = list.Head;

			while (current != null)
			{
				if (content.CompareTo(current.Content) > 0)
				{
					if (listBeforeHead == null)
					{
						listBeforeHead = currentBefore = current;
					}
					else
					{
						currentBefore.Next = current;
						currentBefore = current;
					}
				}
				else
				{
					if (listAfterHead == null)
					{
						listAfterHead = currentAfter = current;
					}
					else
					{
						currentAfter.Next = current;
						currentAfter = current;
					}
				}

				current = current.Next;
			} // end while

			if (listBeforeHead != null)
			{
				list.Head = listBeforeHead;
				currentBefore.Next = listAfterHead;
			}
			else if (listAfterHead != null)
			{
				list.Head = listAfterHead;

			}

			if (currentAfter != null)
			{
				currentAfter.Next = null;
			}
		}

		/// <summary>
		/// Checks if given list is palindrome
		/// </summary>
		/// <typeparam name="T">Content type</typeparam>
		/// <param name="list">Given list</param>
		/// <returns>First intersection node</returns>
		public static bool IsPalindrome<T>(this Helpers.LinkedList<T> list)
		{
			if (list == null || list.IsEmpty)
			{
				return false;
			}

			var fast = list.Head;
			var slow = list.Head;
			var stack = new Stack<T>();

			while (fast != null && fast.Next != null)
			{
				stack.Push(slow.Content);
				slow = slow.Next;
				fast = fast.Next.Next;
			}

			// odd length
			if (fast != null)
			{
				slow = slow.Next;
			}

			while (stack.Count > 0)
			{
				if (!stack.Pop().Equals(slow.Content))
				{
					return false;
				}

				slow = slow.Next;
			}

			return true;
		}

		/// <summary>
		/// Returns start of loop if it exists
		/// </summary>
		/// <typeparam name="T">Content type</typeparam>
		/// <param name="list">Linked list</param>
		/// <returns>Start of loop is exists</returns>
		public static Helpers.LinkedListNode<T> GetStartOfLoop<T>(this Helpers.LinkedList<T> list)
		{
			if (list == null || list.IsEmpty)
			{
				return null;
			}

			var slow = list.Head;
			var fast = list.Head;

			// after K steps: slow is on the beginning of the loop, fast in the loop. 
			// Distance between them = loop length - k
			// after more LL - k steps, they are collised at K steps before loop start
			while (fast != null && fast.Next != null)
			{
				slow = slow.Next;
				fast = fast.Next.Next;

				if (fast == slow)
				{
					break;
				}
			}

			// no loop
			if (fast == null || fast.Next == null)
			{
				return null;
			}

			// move slow to Head and go K steps until collision = loop start
			slow = list.Head;
			while (slow != fast)
			{
				slow = slow.Next;
				fast = fast.Next;
			}

			return slow;
		}

		#region Others

		/// <summary>
		/// Remove given element from list
		/// </summary>
		/// <typeparam name="T">Type of list's content</typeparam>
		/// <param name="node">Linked list node</param>
		public static void RemoveFromMiddle<T>(this Helpers.LinkedListNode<T> node)
		{
			if (node == null || node.Next == null)
			{
				return;
			}

			node.Content = node.Next.Content;
			node.Next = node.Next.Next;
		}

		/// <summary>
		/// Calculates sum of numbers stored on linked list in reversed order
		/// </summary>
		/// <param name="a">First summand</param>
		/// <param name="b">Second summand</param>
		/// <returns>Sum result in linked list</returns>
		public static Helpers.LinkedList<int> SumReversed(this Helpers.LinkedList<int> a, Helpers.LinkedList<int> b)
		{
			if (a == null || a.IsEmpty)
			{
				return b;
			}

			if (b == null || b.IsEmpty)
			{
				return a;
			}

			var result = new Helpers.LinkedList<int>();
			var overflow = 0;
			var first = a.Head;
			var second = b.Head;

			while (first != null || second != null)
			{
				var sum = (first?.Content ?? 0) + (second?.Content ?? 0) + overflow;
				var currentValue = sum % 10;
				overflow = sum / 10;
				result.AddToEnd(currentValue);

				first = first?.Next;
				second = second?.Next;
			}

			if (overflow > 0)
			{
				result.AddToEnd(overflow);
			}

			return result;
		}

		/// <summary>
		/// Calculates sum of numbers stored on linked list in ordinal order
		/// </summary>
		/// <param name="a">First summand</param>
		/// <param name="b">Second summand</param>
		/// <returns>Sum result in linked list</returns>
		public static Helpers.LinkedList<int> Sum(this Helpers.LinkedList<int> a, Helpers.LinkedList<int> b)
		{
			if (a == null || a.IsEmpty)
			{
				return b;
			}

			if (b == null || b.IsEmpty)
			{
				return a;
			}

			int length1 = a.Length;
			int length2 = b.Length;

			if (length1 < length2)
			{
				padLeft(a, length2 - length1);
			}

			if (length2 < length1)
			{
				padLeft(b, length1 - length2);
			}

			var result = sumHelper(a.Head, b.Head);
			if (result.Overflow > 0)
			{
				result.result.AddToBegin(result.Overflow);
			}

			return result.result;
		}

		private static void padLeft(Helpers.LinkedList<int> list, int count)
		{
			for (var i = 0; i < count; i++)
			{
				list.AddToBegin(0);
			}
		}

		private static SumResult sumHelper(Helpers.LinkedListNode<int> a, Helpers.LinkedListNode<int> b)
		{
			if (a == null || b == null)
			{
				return new SumResult();
			}

			var result = sumHelper(a.Next, b.Next);

			var sum = result.Overflow + a.Content + b.Content;
			result.result.AddToBegin(sum % 10);
			result.Overflow = sum / 10;

			return result;
		}

		/// <summary>
		/// Return node of intersection (by ref) between lists. If no intersection - return null
		/// </summary>
		/// <typeparam name="T">Content type</typeparam>
		/// <param name="list1">First list</param>
		/// <param name="list2">Second list</param>
		/// <returns>Node of intersection</returns>
		public static Helpers.LinkedListNode<T> GetIntersection<T>(this Helpers.LinkedList<T> list1, Helpers.LinkedList<T> list2)
		{
			if (list1 == null || list1.IsEmpty || list2 == null || list2.IsEmpty)
			{
				return null;
			}

			var countAndTail1 = GetCountAndTail(list1);
			var countAndTail2 = GetCountAndTail(list2);

			if (countAndTail1.Tail != countAndTail2.Tail)
			{
				return null;
			}

			var shorter = countAndTail1.Count < countAndTail2.Count
				? list1.Head : list2.Head;

			var longer = countAndTail1.Count < countAndTail2.Count
				? list2.Head : list1.Head;

			for (var i = 0; i < Math.Abs(countAndTail1.Count - countAndTail2.Count); i++)
			{
				longer = longer.Next;
			}

			while (shorter != null)
			{
				if (shorter == longer)
				{
					return shorter;
				}

				shorter = shorter.Next;
				longer = longer.Next;
			}

			return null;
		}

		private static CountAndTail<T> GetCountAndTail<T>(Helpers.LinkedList<T> list)
		{
			var result = new CountAndTail<T>();
			var current = list.Head;

			while (current != null)
			{
				result.Count++;
				result.Tail = current;
				current = current.Next;
			}

			return result;
		}

		#endregion
	}

	class SumResult
	{
		public Helpers.LinkedList<int> result;
		public int Overflow;

		public SumResult()
		{
			this.result = new Helpers.LinkedList<int>();
			this.Overflow = 0;
		}
	}

	class CountAndTail<T>
	{
		public int Count = 0;
		public Helpers.LinkedListNode<T> Tail;
	}
}
