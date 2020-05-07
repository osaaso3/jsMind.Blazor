﻿using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;
using JsMind.Blazor.Events;
using JsMind.Blazor.Events.Interop;
using JsMind.Blazor.Models;
using Microsoft.JSInterop;

namespace JsMind.Blazor.Components
{
    public partial class MindMapContainer<T>
    {
        [JSInvokable]
        public async ValueTask OnShowCallback(InteropEventData evt)
        {
        }

        [JSInvokable]
        public async ValueTask OnResizeCallback(InteropEventData evt)
        {
        }

        [JSInvokable]
        public async ValueTask OnEditCallback(InteropEventData evt)
        {
            switch (evt.Type)
            {
                case "add_nodeXXX":
                    // this.invoke_event_handle(jm.event_type.edit, { evt: 'add_node', data: [parent_node.id, nodeid, topic, data], node: nodeid });
                    await OnAddNode.InvokeAsync(new MindMapAddNodeEventArgs<T>
                    {
                        Node = FindNode(evt.NodeId),
                        ParentId = evt.Data[0].GetString(),
                        NodeId = evt.Data[1].GetString(),
                        Topic = evt.Data[2].GetString(),
                        Data = JsonSerializer.Deserialize<IDictionary<string, string>>(evt.Data[3].GetRawText())
                    });
                    break;
            }
        }

        [JSInvokable]
        public async ValueTask OnSelectCallback(InteropEventData evt)
        {
            switch (evt.Type)
            {
                case "select_node":
                    // this.invoke_event_handle(jm.event_type.select, { evt: 'select_node', data: [], node: node.id });
                    await OnSelectNode.InvokeAsync(new MindMapEventArgs<T> { Node = FindNode(evt.NodeId) });
                    break;
            }
        }

        protected abstract T? FindNode(string id);
    }
}