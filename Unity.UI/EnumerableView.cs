using System.Collections;
using System.Collections.Generic;

namespace UnityEngine.UI
{
	public class EnumerableView : DataBoundElement
	{
		public enum Direction
		{
			Vertical,
			Horizontal
		}

		[SerializeField]
		private DataBoundElement template = null;

		public Direction direction { get; set; }
		public DataBoundElement[] elements { get; private set; }

		public void Clear()
		{
			if (elements != null)
			{
				foreach (DataBoundElement e in elements)
				{
					Destroy(e.gameObject);
				}
				elements = null;
			}
		}

		public override void Draw()
		{
			IEnumerable objects = GetDataAs<IEnumerable>();
			List<DataBoundElement> elements = new List<DataBoundElement>();

			if (objects != null && template != null)
			{
				Clear();

				float offset = 0;
				foreach (object o in objects)
				{
					DataBoundElement e = Instantiate(template, transform);
					elements.Add(e);

					if (direction == Direction.Vertical)
					{
						e.ShiftDown(offset);
						offset += e.height;
					}
					else //direction == Direction.Horizontal
					{
						e.ShiftRight(offset);
						offset += e.width;
					}

					e.gameObject.SetActive(true);
					e.Bind(o);
					e.Draw();
				}

				this.elements = elements.ToArray();
			}
		}
	}
}
