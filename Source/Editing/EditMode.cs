
#region ================== Copyright (c) 2007 Pascal vd Heiden

/*
 * Copyright (c) 2007 Pascal vd Heiden, www.codeimp.com
 * This program is released under GNU General Public License
 * 
 * This program is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 * 
 */

#endregion

#region ================== Namespaces

using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using CodeImp.DoomBuilder.Interface;
using CodeImp.DoomBuilder.IO;
using CodeImp.DoomBuilder.Map;
using CodeImp.DoomBuilder.Rendering;
using System.Diagnostics;
using CodeImp.DoomBuilder.Controls;

#endregion

namespace CodeImp.DoomBuilder.Editing
{
	internal class EditMode : IDisposable
	{
		#region ================== Constants

		#endregion

		#region ================== Variables
		
		// Graphics
		protected D3DGraphics graphics;
		
		// Disposing
		protected bool isdisposed = false;

		#endregion

		#region ================== Properties

		// Disposing
		public bool IsDisposed { get { return isdisposed; } }

		#endregion

		#region ================== Constructor / Disposer

		// Constructor
		public EditMode()
		{
			// Initialize
			this.graphics = General.Map.Graphics;
			
			// Bind any methods
			ActionAttribute.BindMethods(this);
			
			// We have no destructor
			GC.SuppressFinalize(this);
		}

		// Diposer
		public virtual void Dispose()
		{
			// Not already disposed?
			if(!isdisposed)
			{
				// Unbind any methods
				ActionAttribute.UnbindMethods(this);
				
				// Clean up

				// Done
				isdisposed = true;
			}
		}

		#endregion

		#region ================== Static Methods

		// This creates an instance of a specific mode
		public static EditMode Create(Type modetype, object[] args)
		{
			try
			{
				// Create new mode
				return (EditMode)General.ThisAssembly.CreateInstance(modetype.FullName, false,
					BindingFlags.Default, null, args, CultureInfo.CurrentCulture, new object[0]);
			}
			// Catch errors
			catch(TargetInvocationException e)
			{
				// Throw the actual exception
				Debug.WriteLine(DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToShortTimeString());
				Debug.WriteLine(e.InnerException.Source + " throws " + e.InnerException.GetType().Name + ":");
				Debug.WriteLine(e.InnerException.Message);
				Debug.WriteLine(e.InnerException.StackTrace);
				throw e.InnerException;
			}
		}
		
		#endregion

		#region ================== Methods

		// Optional interface methods
		public virtual void MouseClick(MouseEventArgs e) { }
		public virtual void MouseDoubleClick(MouseEventArgs e) { }
		public virtual void MouseDown(MouseEventArgs e) { }
		public virtual void MouseEnter(EventArgs e) { }
		public virtual void MouseLeave(EventArgs e) { }
		public virtual void MouseMove(MouseEventArgs e) { }
		public virtual void MouseUp(MouseEventArgs e) { }
		public virtual void Cancel() { }
		public virtual void RedrawDisplay() { }
		
		#endregion
	}
}
