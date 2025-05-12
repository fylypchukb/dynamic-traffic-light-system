import React from "react";
import {AlertCircle} from "lucide-react";
import {Button} from "@/components/ui/button";
import Link from "next/link";

interface EmptyStateProps {
    title: string;
    description?: string;
    icon?: React.ReactNode;
    actionHref?: string;
    actionText?: string;
}

export function EmptyState({
                               title,
                               description,
                               icon = <AlertCircle className="h-10 w-10 text-muted-foreground"/>,
                               actionHref,
                               actionText = "Create New",
                           }: EmptyStateProps) {
    return (
        <div className="flex flex-col items-center justify-center h-64 p-8 text-center border rounded-lg bg-background">
            <div className="mb-4">{icon}</div>
            <h3 className="text-lg font-medium">{title}</h3>
            {description && (
                <p className="mt-2 text-sm text-muted-foreground max-w-md">{description}</p>
            )}
            {actionHref && (
                <Button asChild className="mt-6">
                    <Link href={actionHref}>{actionText}</Link>
                </Button>
            )}
        </div>
    );
}