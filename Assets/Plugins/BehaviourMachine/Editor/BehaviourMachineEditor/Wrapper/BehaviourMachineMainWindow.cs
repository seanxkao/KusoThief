//----------------------------------------------
//            Behaviour Machine
// Copyright © 2014 Anderson Campos Cardoso
//----------------------------------------------

using UnityEngine;
using UnityEditor;
using System.Collections;
using System;
using BehaviourMachineEditor;
using System.Linq;
using BehaviourMachine;
using System.Collections.Generic;

/// <summary>
/// Wrapper class for Behaviour window.
/// <summary>
class BehaviourMachineMainWindow : BehaviourMachineEditor.BehaviourWindow
{
    private string m_searchNode;

    protected override void DrawSearch()
    {
        base.DrawSearch();
        GUILayout.Space(18);
        GUILayout.BeginHorizontal();
        {
            if (activeParent != null && activeParent.root != null)
            {
                EditorGUI.BeginChangeCheck();
                GUI.SetNextControlName("SearchNode");
                m_searchNode = GUILayout.TextField(m_searchNode, GUILayout.Width(200), GUILayout.Height(18));
                if (Event.current.isKey && Event.current.keyCode == KeyCode.Return && GUI.GetNameOfFocusedControl() == "SearchNode")
                {
                    SearchNode();
                }

                if (GUILayout.Button("Search", GUILayout.Width(200)))
                {
                    SearchNode();
                }
                if (GUILayout.Button("Clear", GUILayout.Width(200)))
                {
                    m_searchNode = "";
                }
            }
        }
        GUILayout.EndHorizontal();
    }

    private void SearchNode()
    {
        Debug.Log("SearchNode:" + m_searchNode);
        if (BehaviourWindow.activeTree != null)
        {
            SearchInTree(BehaviourWindow.activeTree);
        }
        else if (BehaviourWindow.activeFsm != null)
        {
            SearchInFsm(BehaviourWindow.activeFsm);
        }
    }

    private void SearchInFsm(InternalStateMachine stateMachine)
    {
        var fsm = BehaviourWindow.activeFsm;
        var states = fsm.states;

        foreach (var state in states)
        {
            var hasMatchNode = false;
            if (state is InternalBehaviourTree)
            {
                var tree = state as InternalBehaviourTree;
                hasMatchNode = tree.GetNodes().Where(e => e.GetType().Name == m_searchNode).Any();
            }
            else if (state is InternalActionState)
            {
                hasMatchNode = ((InternalActionState)state).GetNodes().Where(e => e.GetType().Name == m_searchNode).Any();
            }
            else
            {
                Debug.Log(state.stateName + " not tree or action!");
            }

            state.searchColor = StateColor.Grey;
            if (hasMatchNode)
            {
                state.searchColor = StateColor.Red;
            }
        }
    }

    private void SearchInTree(InternalBehaviourTree tree)
    {
        var nodes = tree.GetNodes();
        foreach (var node in nodes)
        {
            node.IsMatchSearch = false;
            if (node.GetType().Name.Equals(m_searchNode))
            {
                node.IsMatchSearch = true;
            }
        }
    }
}
